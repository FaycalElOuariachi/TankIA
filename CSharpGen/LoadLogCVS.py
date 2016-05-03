# LoadLogCVS
import os

SpaceChara = ';'

class CounterCVS():

    def concatenateFiles(self, dirname, filename):
        if not os.path.exists(filename):
            flag = True
        else:
            flag = False

        BigFile = open(filename, 'a')

        for file in os.listdir(dirname):
            f = open(file)
            if flag:
                lines = f.readlines()
                flag = False
            else:
                lines = f.readlines()[1:]
            BigFile.writelines(lines)

        BigFile.close()

    def countAllFiles(self, dirlogname, dirname):
        self.loadForm(dirlogname)
        # for files in dirname
        file =""

        tabOfSingleFile = self.countSingleFile(file)
        self.add_Single_Tab_To_Global_Tab(tabOfSingleFile, 1)



    def countSingleFile(self, filename):

        tab = self.original_Tab.copy()

        f = open(filename, 'r')
        line = f.readline()
        while (line):
            line = f.readline().split(SpaceChara)
            item = tab
            for i in range(1,self.nbVaria-1):
                item = item[line[self.nbVaria - i]]
            item[line[0]] += 1

        f.close()

        return tab

    def loadForm(self, filename):
        f = open(filename, 'r')

        self.nbVaria = int(f.readline().rstrip('\n'))

        self.global_Tab = 1 # ----------- # tab = 0
        self.indices = []
        for i in range(self.nbVaria):
            line = f.readline()
            sizeVaria = line.split(SpaceChara)[1]
            self.indices += [sizeVaria]
            if i == 0:
                self.global_Tab = [ self.global_Tab for i in range(sizeVaria)]
            elif isinstance(self.original_Tab, list):
                self.global_Tab = [ self.global_Tab.copy() for i in range(sizeVaria)]

        self.indices.reverse()

        f.close()

    def add_Single_Tab_To_Global_Tab(self, tabOfSingleFile, weight):
        tmp = self.global_Tab

        for i in range(self.nbVaria):
            for j in range(self.indices[i]):
                return
