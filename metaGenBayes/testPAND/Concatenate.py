import os
from os.path import basename
import pyAgrum as gum
import pyAgrum.lib.notebook as gnb

def concatenateFiles():
        #Allpaths
        path = 'logs/Log/WholeLog.csv'
        pathAllFiles = 'logs'
        if not os.path.exists(path):
            found = False
        else:
            found = True


        #BigFile est le fichier qui aura tous les logs

        BigFile = open(path, 'a')



        OneDone = False
        for file in os.listdir(pathAllFiles):
            if basename(file)!="Log" and os.path.splitext(file)[1]!=".meta":
                for file1 in os.listdir(pathAllFiles+"/"+file):
                    fileNameAndExtension = os.path.splitext(file1)
                    newPath = pathAllFiles+"/"+file+"/"
                    #test du nom de l'extension
                    if fileNameAndExtension[1]==".csv":
                        f = open(newPath+file1,'r')
                        if found==False and OneDone==False:
                            OneDone=True
                            lines = f.readlines()
                        else:
                            lines = f.readlines()[1:]
                        BigFile.writelines(lines)
        BigFile.close()




def createBayesianNetwork():
    learner=gum.BNLearner("logs/Log/WholeLog.csv")
    learner.useK2([0,1,2,3,4,5,6,7,8,9,10,11,12])
    bn2=learner.learnBN()
    print("Learned in {0}s".format(learner.currentTime()))
    gnb.showBN(bn2)
    return bn2


def saveBayesianNetwork():
    convertFileToCSV()
    concatenateFiles()
    bn = createBayesianNetwork()
    bn.saveBIF("IA_bayesian_network.bif")

def convertFileToCSV():
    path = 'dir_Log'
    for file in os.listdir(path):
        directoryName = os.path.splitext(file)
        if basename(directoryName[0])!="Log":
            for log in os.listdir(path+"/"+basename(directoryName[0])):
                logExtension = os.path.splitext(log)[1]
                logName = os.path.splitext(log)[0]

                if logExtension==".ASCIIlog":
                    os.rename(path+"/"+basename(directoryName[0])+"/"+log, path+"/"+basename(directoryName[0])+"/"+logName + ".csv")




concatenateFiles()
#createBayesianNetwork()
#saveBayesianNetwork()
#convertFileToCSV()