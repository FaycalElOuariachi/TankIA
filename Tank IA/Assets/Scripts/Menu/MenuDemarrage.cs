using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class MenuDemarrage : MonoBehaviour {

    //public Button[] buttons;
    private bool hide = false;
    private bool hideStart = false;

    public Dropdown choiceGame;
    public Dropdown choiceParty;
    private string m_PathRecorder = @"records";
    private string[] filesRecords;
    private string recordName;
    private Dropdown[] listDropdown;
    private Toggle[] choiceUser;
    private Toggle player1Confirm;
    private Toggle player2Confirm;
    public static bool isPlayer1Static = false;
    public static bool isPlayer2Static = false;

    private GameObject listReplayText;
    private GameObject listParties;
    private GameObject player1Text;
    private GameObject player2Text;
    


    // Use this for initialization
    void Start () {
       
       

    }

    void Awake()
    {
        //placer l'image de départ et redimensionner
        Image t = GetComponentInChildren<Image>();
        t.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);


        listDropdown = GetComponentsInChildren<Dropdown>();

        for (int i = 0; i < listDropdown.Length; i++)
        {
            if (listDropdown[i].name.Equals("ListReplays"))
            {
                choiceGame = listDropdown[i];
            }
            if (listDropdown[i].name.Equals("ListParties"))
            {
                choiceParty = listDropdown[i];
            }
        }

       choiceUser = GetComponentsInChildren<Toggle>();

        for (int i = 0; i < choiceUser.Length; i++)
        {
            if (choiceUser[i].name.Equals("IAPlay1"))
            {
                player1Confirm = choiceUser[i];
            }
            if (choiceUser[i].name.Equals("IAPlay2"))
            {
                player2Confirm = choiceUser[i];
            }
        }
        
       
        //recherche des éléments UI
        listReplayText = GameObject.Find("Replays");
        listParties = GameObject.Find("Party");
        player1Text = GameObject.Find("Player1");
        player2Text = GameObject.Find("Player2");

        //placement des éléments dans l'interface graphique
        player1Text.transform.localPosition = new Vector3(-75, 90, 0);
        player2Text.transform.localPosition = new Vector3(-75, 50, 0);
        player1Confirm.transform.localPosition = new Vector3(-5, 95, 0);
        player2Confirm.transform.localPosition = new Vector3(-5, 55, 0);
        listReplayText.transform.localPosition = new Vector3(-75, 90, 0);
        listParties.transform.localPosition = new Vector3(-55, 45, 0);

        //désactiver les objets
        listReplayText.gameObject.SetActive(false);
        player1Confirm.gameObject.SetActive(false);
        player2Confirm.gameObject.SetActive(false);
        listParties.gameObject.SetActive(false);
        player1Text.gameObject.SetActive(false);
        player2Text.gameObject.SetActive(false);


        //effacer les options de la liste du choix du jeu
        choiceGame.options.Clear();

        //ajouter des valeurs dans les listes
        populateList();


        choiceGame.gameObject.SetActive(false);
        choiceParty.gameObject.SetActive(false);
        choiceGame.transform.localPosition = new Vector3(0, 100, 0);
        choiceParty.transform.localPosition = new Vector3(0, 50, 0);



    }

    // Update is called once per frame
    void Update () {
        

    }


    



    public void populateParties()
    {
        choiceParty.options.Clear();
        string recordPath = choiceGame.captionText.text;
        
        string[] files = Directory.GetFiles(m_PathRecorder + "/" + recordPath);

        for (int i = 0; i < files.Length; i++)
        {
            string fileName = Path.GetFileName(files[i]);
            Dropdown.OptionData recordFileOption = new Dropdown.OptionData(fileName);
            choiceParty.options.Add(recordFileOption);
            choiceParty.value = 0;
        }
    }

   
    public void populateList()
    {
        filesRecords = Directory.GetDirectories(m_PathRecorder);
        for (int i = 0; i < filesRecords.Length; i++)
        {

            recordName = filesRecords[i].Substring(8);
            Dropdown.OptionData recordOption = new Dropdown.OptionData(recordName);
            choiceGame.options.Add(recordOption);
            choiceGame.value = 0;
        }
    }

   
    //méthode pour lancer le jeu lors du clique
    public void startGame()
    {

        isPlayer1Static = player1Confirm.isOn;
        isPlayer2Static = player2Confirm.isOn;
		//Application.LoadLevel(1);
		SceneManager.LoadScene(1);
    }

	public void replayGame( int numeroReplay)//string nameReplay)
    {

		ScenesParameters.m_GameNumber = numeroReplay + 1;
		//Debug.Log (numeroReplay + 1);//.Substring (4));
		//Application.LoadLevel(1);
		SceneManager.LoadScene(1);
        

    }

    public void quitGame()
    {
        Application.Quit();
    }


   

    private void OnGUI()
    {

     

        //start game
        if (hide == false && hideStart==false)
        {
                if (GUI.Button(new Rect(Screen.width - 200, Screen.height - 200, 160, 30), "Start"))
            {
                	//Application.LoadLevel(1);

                    player1Text.gameObject.SetActive(true);
                    player1Confirm.gameObject.SetActive(true);
                    player2Confirm.gameObject.SetActive(true);
                    player2Text.gameObject.SetActive(true);



                	hideStart = true;
                    hide = true;
                
               
               


            }

        // replay
        
            if (GUI.Button(new Rect(Screen.width - 200, Screen.height - 150, 160, 30), "Replay"))
            {
                hide = true;
                choiceGame.gameObject.SetActive(true);
                choiceParty.gameObject.SetActive(true);
                listReplayText.gameObject.SetActive(true);
                listParties.gameObject.SetActive(true);
            }


            //quit game
            if (GUI.Button(new Rect(Screen.width - 200, Screen.height - 100, 160, 30), "Quit"))
            {
                quitGame();
            }
        }


        //si on est dans le replay
        if (hide == true && hideStart == false)
        {

            if (GUI.Button(new Rect(Screen.width / 2 + 100, Screen.height / 2, 160, 30), "Start Replay"))
            {

				replayGame ( choiceGame.value ); //choiceGame.itemText.text);

            }

            if (GUI.Button(new Rect(Screen.width / 2 - 250, Screen.height / 2, 160, 30), "Back"))
            {

                hide = false;
                choiceGame.gameObject.SetActive(false);
                choiceParty.gameObject.SetActive(false);
                listReplayText.gameObject.SetActive(false);
                listParties.gameObject.SetActive(false);


            }
        }
            //si on est dans le start
        if (hide == true && hideStart == true)
            {

                if (GUI.Button(new Rect(Screen.width / 2 + 100, Screen.height / 2, 160, 30), "Start Game"))
                {

                    startGame();

                }

                if (GUI.Button(new Rect(Screen.width / 2 - 250, Screen.height / 2, 160, 30), "Back"))
                {

                    hide = false;
                    hideStart = false;
                    player1Text.gameObject.SetActive(false);
                    player1Confirm.gameObject.SetActive(false);
                    player2Confirm.gameObject.SetActive(false);
                    player2Text.gameObject.SetActive(false);



                }



            }
        
    }
}
