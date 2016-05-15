using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class GameManager : MonoBehaviour
{
	/**
	 * --- Valeur quantitative
	 * Num Rounds To Win     : nombre de round à gagner pour finir une partie
	 * Start Delay           : temps minimum de chargement pré-round
	 * End Delay             : temps minimum de chargement post-round
	 * 
	 * --- Mise en place de la scène
	 * Camera Control        : CameraRig de la scène, qui servira à bien positionner la caméra
	 * 					      en fonction de la position des tanks
	 * Message Text          : écran d'affichage, possibilité d'en changer le texte à tout moment
	 * 			   		     : notamment lors des pré-rounds et des post-rounds
	 * 
	 * --- Prefabs
	 * Tank Prefab		  	: prefab de Tank à instancier
	 * IA Tank Prefab		: prefab de IA Tank à instancier
	 * Tank Replay Prefab  	: prefab de Tank Replay à instancier
	 * Recorder Prefab    	: prefab de recorder à instancier
	 * Logger Prefab   		: prefab de logger à instancier
	 * Logger Manager Prefab	: prefab de logger Manager à instancier
	 * 
	 * --- Managers
	 * Tanks			   		: liste de Tank Manager
	 * Recorder Manager      : liste de recorder manager, à instancier ou non
	 * Logger Manager      	: liste de logger manager, à instancier ou non
	 * Replay Manager   		: replay manager, à instancier ou non
	 * 
	 * Player Mask		   		: Mask des joueurs
	 * m_MaskOn					: Booleen indiquant si c'est un replay
	 * 
	 * --- Variables d'indication de mode (normal, replay, record)
	 * Has Recorder          : booléen indiquant la présence de Recorders
	 * Game Number  	     : numéro de la partie à rejouer. Si la valeur est -1, ce n'est pas un
	 * 					     : replay
	 * IA Tank One		     : nom de l'IA à utiliser pour le premier tank
	 * IA Tank Two		     : nom de l'IA à utiliser pour le deuxième tank
	 */
    public int m_NumRoundsToWin = 5;        
    public float m_StartDelay = 3f;         
    public float m_EndDelay = 3f;           

    public CameraControl m_CameraControl;   
    public Text m_MessageText;              

	public GameObject m_TankPrefab; 
	public GameObject m_TankReplayPrefab; 
	public GameObject m_IATankPrefab; 
	public GameObject m_RecorderPrefab;        
	public GameObject m_LoggerManagerPrefab;            
	public GameObject m_LoggerPrefab;    
    
	public TankManager[] m_Tanks;
	public RecorderManager[] m_Recorders;
	public LoggerManager[] m_Loggers;
	public ReplayManager m_ReplayManager = null;

	public LayerMask m_PlayerMask;
	public bool m_MaskOn = false;

	public bool m_HasRecorder = true;
	public int m_GameNumber = -1;  
	public string m_GameName = "";  
	[HideInInspector] public string[] m_IATanks;



	/**
	 * --- Timeout
	 * Start Wait		  : temps minimum de chargement pré-round, basé sur Start Delay
	 * End Wait			  : temps minimum de chargement pré-round, basé sur End Delay
	 * 
	 * --- Replay
	 * Recorder Path	  : chemin du dossier contenant les logs des replays
	 * NbPlayer			  : nombre de joueur de la partie du replay
	 * 
	 * --- Infos sur l'état de la partie
	 * Round Number       : numéro du round courant
	 * Round Winner		  : vainqueur du dernier round
	 * Game Winner		  : vainqueur de la partie
	 */
    private WaitForSeconds m_StartWait;     
	private WaitForSeconds m_EndWait;   

	private string m_RecorderPath;
	private int m_NbPlayer;    

	private int m_RoundNumber;  
	private TankManager m_RoundWinner;
	private TankManager m_GameWinner;      
	private int m_TimeReference;

    
	/**
	 * Fonction appelée lors de la création d'une instance de la classe, initiateur d'une partie
	 * ---
	 * • Récupère les paramètres choisis par l'utilisateur via un menu
	 * • Met en place les delais d'attentes des phase pré-rounds et post-rounds
	 * • Initialise tous les tanks, la caméra et les recorders s'il y en a
	 * • Lance la partie
	 */
    private void Start()
    {
		m_TimeReference = Time.frameCount + 20;
		m_HasRecorder = ScenesParameters.m_HasRecorder;

		//ScenesParameters.m_GameName = ""; // Force à ne pas rejouer de replay

		if (ScenesParameters.m_GameNumber != -1)
			m_GameNumber = ScenesParameters.m_GameNumber;

		if (ScenesParameters.m_GameName != "") {
			m_HasRecorder = false;
			m_GameName = ScenesParameters.m_GameName;
			/*if (m_MaskOn) {
				Physics.IgnoreLayerCollision (m_PlayerMask, m_PlayerMask, true);
			}*/
		}

		m_IATanks = ScenesParameters.m_IATanks;

		// Forcer à ne pas utiliser d'IA
		//m_IATanks[0] = m_IATanks[1] = "";

        m_StartWait = new WaitForSeconds(m_StartDelay);
		m_EndWait = new WaitForSeconds(m_EndDelay);

		//if (m_GameNumber != -1) {
		if (m_GameName != "") {
			//Check if game exists
			//m_RecorderPath = @"records" + "\\Game" + m_GameNumber;
			m_RecorderPath = m_GameName; //@"records" + Path.AltDirectorySeparatorChar + m_GameName;
			if (!Directory.Exists (m_RecorderPath)) {
				m_MessageText.text = m_RecorderPath + "\n Unknown Game";
				return;
			}

			//Find the number of tanks in records
			m_NbPlayer = Directory.GetFiles (m_RecorderPath, "Round1_?.IArec").GetLength (0);

			//Compare the number of tanks Unity VS RecordsS
			if (m_NbPlayer != m_Tanks.Length) {
				m_MessageText.text = "Incorrect Number of tanks";
			}

		}

		SpawnAllTanks();
		SetCameraTargets();

		if (ScenesParameters.m_Logger != "") {
			SetAllLoggers ();
		}

		if (m_HasRecorder || ScenesParameters.m_Logger != "") {
			SetAllRecorders ();
		}


        StartCoroutine(GameLoop());
    }


	/**
	 * Instancie les tanks en leur attribuant un numéro de joueur différent
	 */
    private void SpawnAllTanks()
    {

        for (int i = 0; i < m_Tanks.Length; i++)
        {
			// Si l'on joue un replay
			if (m_GameName != "") {
				m_Tanks [i].m_Instance =
					Instantiate (m_TankReplayPrefab, m_Tanks [i].m_SpawnPoint.position, m_Tanks [i].m_SpawnPoint.rotation) as GameObject;
				m_Tanks [i].m_RecorderPath = m_RecorderPath;
				m_Tanks [i].m_isReplay = true;
				m_Tanks [i].m_TimeReference = m_TimeReference;
			}
			// Sinon, si une IA a été sélectinner (dans ScenesParameters)
			else if (m_IATanks [i] != "") {
				m_Tanks [i].m_Instance =
					Instantiate (m_IATankPrefab, m_Tanks [i].m_SpawnPoint.position, m_Tanks [i].m_SpawnPoint.rotation) as GameObject;
			}
			// Sinon, mode normal
			else {
				m_Tanks [i].m_Instance =
					Instantiate (m_TankPrefab, m_Tanks [i].m_SpawnPoint.position, m_Tanks [i].m_SpawnPoint.rotation) as GameObject;
			}
            m_Tanks[i].m_PlayerNumber = i + 1;
            m_Tanks[i].Setup();
        }
	}

	/**
	 * Instancie les recorders pour chaque tanks
	 */
	private void SetAllRecorders()
	{
		for (int i = 0; i < m_Tanks.Length; i++)
		{
			m_Recorders [i].m_PlayerNumber = i + 1;
			m_Recorders [i].m_Instance = Instantiate (m_RecorderPrefab) as GameObject;
			m_Recorders [i].Setup ();
			m_Recorders [i].SetTankInstance(m_Tanks [i]);
			m_Recorders [i].setTimeReference (m_TimeReference);
			if (ScenesParameters.m_Logger != "") {
				m_Recorders [i].setLog(m_Loggers [i].m_Logger.m_Logger);
			}
		}
	}

	/**
	 * Instancie les loggers pour chaque tanks
	 */
	private void SetAllLoggers()
	{
		for (int i = 0; i < m_Loggers.Length; i++)
		{
			m_Loggers [i] = Instantiate (m_LoggerManagerPrefab).GetComponent<LoggerManager>();
			m_Loggers [i].m_PlayerNumber = i + 1;
			m_Loggers [i].m_Instance = Instantiate (m_LoggerPrefab) as GameObject;
			m_Loggers [i].Setup ();
			m_Loggers [i].SetTank(m_Tanks [i], m_Tanks[(i+1)%2]);
			m_Loggers [i].setTimeReference(m_TimeReference);
		}
	}

	/**
	 * Associe les tanks au positionneur de la caméra (Camera Rig)
	 */
    private void SetCameraTargets()
    {
        Transform[] targets = new Transform[m_Tanks.Length];

        for (int i = 0; i < targets.Length; i++)
        {
            targets[i] = m_Tanks[i].m_Instance.transform;
        }

        m_CameraControl.m_Targets = targets;
    }

	/**
	 * Loop principale du jeu
	 */
    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        if (m_GameWinner != null)
        {
			//Application.LoadLevel (Application.loadedLevel);
			//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			if (m_ReplayManager != null) {
				m_ReplayManager.setGo ();
				Destroy (this);
			}
			if (m_ReplayManager == null)
				SceneManager.LoadScene(0);
        }
        else
        {
            StartCoroutine(GameLoop());
        }
	}

	/**
	 * Phase de pré-round
	 * • Reset les tanks (et les recorders s'il y en a) et désactive leur control le temps de la phase
	 * • Repositionnne la caméra
	 * • Affiche un message "ROUND [numero du round]"
	 */
    private IEnumerator RoundStarting()
    {
		ResetAllTanks ();
		DisableTankControl ();

		if (m_HasRecorder) {
			ResetAllRecorders ();
			DisableRecorderControl ();
		}

		if (ScenesParameters.m_Logger != "") {
			ResetAllLoggers ();
			DisableLoggerControl ();
		}

		m_CameraControl.SetStartPositionAndSize ();

		m_RoundNumber++;
		m_MessageText.text = "ROUND " + m_RoundNumber;

		yield return m_StartWait;
    }

	/**
	 * Phase principale d'un round
	 * • (Ré-)active les controles des tanks (et des recorders)
	 * • Supprime le message du pré-rounds
	 */
    private IEnumerator RoundPlaying()
	{
		EnableTankControl ();

		if (m_HasRecorder)
			EnableRecorderControl ();


		if (ScenesParameters.m_Logger != "")
			EnableLoggerControl ();

		m_MessageText.text = "";

		while (!OneTankLeft ()) {
			yield return null;
		}
    }

	/**
	 * Phase de post-round
	 * • Désactive le controle des tanks (et les recorders)
	 * • Détermine le vainqueur du round (ou s'il y a match nul)
	 * • Détermine si un tank à gagné la partie
	 * • Affiche un message en conséquence
	 */
    private IEnumerator RoundEnding()
	{
		DisableTankControl ();

		if (m_HasRecorder)
			DisableRecorderControl ();

		if (ScenesParameters.m_Logger != "")
			DisableLoggerControl ();

		m_RoundWinner = null;

		m_RoundWinner = GetRoundWinner ();

		if (m_RoundWinner != null)
			m_RoundWinner.m_Wins++;

		m_GameWinner = GetGameWinner ();

		if (m_ReplayManager == null) {
			string message = EndMessage ();
			m_MessageText.text = message;
		}


		if (m_HasRecorder) {
			// Write into a file the action Player
			for (int i = 0; i < m_Recorders.Length; i++) {
				m_Recorders [i].WriteActions ();
			}
		}

		if (ScenesParameters.m_Logger != "") {
			// Write into a file the action Player
			for (int i = 0; i < m_Loggers.Length; i++) {
				m_Recorders[i].setActionsToLog();
				m_Loggers[i].WriteLog ();
			}
		}

        yield return m_EndWait;
    }

	/**
	 * Renvoie s'il manque au plus 1 tank sur la map (vainqueur ou match nul)
	 */
    private bool OneTankLeft()
    {
        int numTanksLeft = 0;

        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].m_Instance.activeSelf)
                numTanksLeft++;
        }

        return numTanksLeft <= 1;
    }

	/**
	 * Récupère le vainqueur du round, ou null s'il y a match nul
	 */
    private TankManager GetRoundWinner()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].m_Instance.activeSelf)
                return m_Tanks[i];
        }

        return null;
    }

	/**
	 * Renvoie le gagnant de la partie, ou null si la partie n'est pas finie
	 */
    private TankManager GetGameWinner()
    {
		// Si l'on est en replay, et qu'il n'y a plus de round d'enregistré
		if (m_GameName != "" &&
		    !File.Exists (m_GameName + Path.AltDirectorySeparatorChar + "Round" + (m_RoundNumber + 1) + "_1ascii.IArec")) {
			TankManager max = m_Tanks[0];
			for (int i = 0; i < m_Tanks.Length; i++) {
				if (m_Tanks [i].m_Wins > max.m_Wins)
					max = m_Tanks [i];
			}
			if (m_Tanks [0].m_Wins == m_Tanks [1].m_Wins) {
				return null;
			} else {
				return max;
			}
		}

		
			

        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].m_Wins == m_NumRoundsToWin)
                return m_Tanks[i];
        }

        return null;
    }

	/**
	 * Affiche un message à la fin d'un round, en fonction de la situation
	 * • match nul
	 * • gagnant du round
	 * • gagnant de la partie
	 */
    private string EndMessage()
    {
        string message = "DRAW!";

        if (m_RoundWinner != null)
            message = m_RoundWinner.m_ColoredPlayerText + " WINS THE ROUND!";

        message += "\n\n\n\n";

        for (int i = 0; i < m_Tanks.Length; i++)
        {
            message += m_Tanks[i].m_ColoredPlayerText + ": " + m_Tanks[i].m_Wins + " WINS\n";
        }

		if (m_GameWinner != null)
			message = m_GameWinner.m_ColoredPlayerText + " WINS THE GAME!";
		else if (m_GameName != "" && !File.Exists (m_GameName + Path.AltDirectorySeparatorChar + "Round" + (m_RoundNumber + 1) + "_1ascii.IArec")) {
			message = "Replay Finished\nDRAW";
			m_GameWinner = m_Tanks [0];
		}

        return message;
    }

	/**
	 * Ré-initialise tous les tanks
	 */
    private void ResetAllTanks()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].Reset();
        }
	}

	/**
	 * Ré-initialise tous les recorders
	 */
	private void ResetAllRecorders()
	{
		for (int i = 0; i < m_Recorders.Length; i++)
		{
			m_Recorders [i].Reset();
		}
	}

	/**
	 * Ré-initialise tous les loggers
	 */
	private void ResetAllLoggers()
	{
		for (int i = 0; i < m_Loggers.Length; i++)
		{
			m_Loggers [i].Reset();
		}
	}

	/**
	 * (Ré-)active le controle de tous les tanks
	 */
    private void EnableTankControl()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].EnableControl();
        }
	}

	/**
	 * (Ré-)active tous les recorders
	 */
	private void EnableRecorderControl()
	{
		for (int i = 0; i < m_Recorders.Length; i++)
		{
			m_Recorders[i].EnableControl();
		}
	}

	/**
	 * (Ré-)active tous les loggers
	 */
	private void EnableLoggerControl()
	{
		for (int i = 0; i < m_Loggers.Length; i++)
		{
			m_Loggers[i].EnableControl();
		}
	}

	/**
	 * Désactive le controle de tous les tanks
	 */
    private void DisableTankControl()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].DisableControl();
        }
	}

	/**
	 * Désactive tous les recorders
	 */
	private void DisableRecorderControl()
	{
		for (int i = 0; i < m_Recorders.Length; i++)
		{
			m_Recorders[i].DisableControl();
		}
	}

	/**
	 * Désactive tous les loggers
	 */
	private void DisableLoggerControl()
	{
		for (int i = 0; i < m_Loggers.Length; i++)
		{
			m_Loggers[i].DisableControl();
		}
	}

	public void setReplayManager( ReplayManager replayManager ) {
		m_ReplayManager = replayManager;
	}

	public void OnDestroy() {
		for (int i = 0; i < 2; i++) {
			Destroy (m_Tanks [i].m_Instance, 0.1f);
			m_Loggers [i].OnDestroy();
			//m_Tanks [i].OnDestroy();
			m_Recorders [i].OnDestroy();
		}
	}


}