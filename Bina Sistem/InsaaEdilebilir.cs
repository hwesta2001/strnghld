using UnityEngine;

public static class InsaaEdilebilir 
{
    static bool goldOk, woodOk, stoneOk, ironOk;
    static int goldN, woodN, stoneN, ironN;

    public static bool InsaaEtmeKontrol(Bina bina)
    {
        if (Kontrol(bina))
        {
            GlobalResourceEksilt();
            return true;
        }
        else
        {
            TextControl.ins.AlertText("Resource needed M'Lord!", 46, Color.red, 2f);
            return false;
        }

    }


    static bool Kontrol(Bina _bina)
    {
        for (int i = 0; i < _bina.resources.Length; i++)
        {
            if (_bina.resources[i].name == Resource.Name.Gold)
            {
                goldOk = CompareResource(_bina.resources[i].neKadar, Globals.ins.Gold);
                goldN = _bina.resources[i].neKadar * -1;
                break;
            }
            else
            {
                goldN = 0;
                goldOk = true;
            }
        }


        for (int i = 0; i < _bina.resources.Length; i++)
        {
            if (_bina.resources[i].name == Resource.Name.Wood)
            {
                woodOk = CompareResource(_bina.resources[i].neKadar, Globals.ins.Wood);
                woodN = _bina.resources[i].neKadar * -1;
                break;
            }
            else
            {
                woodN = 0;
                woodOk = true;
            }
        }

        for (int i = 0; i < _bina.resources.Length; i++)
        {
            if (_bina.resources[i].name == Resource.Name.Stone)
            {
                stoneOk = CompareResource(_bina.resources[i].neKadar, Globals.ins.Stone);
                stoneN = _bina.resources[i].neKadar * -1;
                break;
            }
            else
            {
                stoneN = 0;
                stoneOk = true;
            }
        }

        for (int i = 0; i < _bina.resources.Length; i++)
        {
            if (_bina.resources[i].name == Resource.Name.Iron)
            {
                ironOk = CompareResource(_bina.resources[i].neKadar, Globals.ins.Iron);
                ironN = _bina.resources[i].neKadar * -1;
                break;
            }
            else
            {
                ironN = 0;
                ironOk = true;
            }
        }


        return goldOk && woodOk && stoneOk && ironOk;

    }

    static void GlobalResourceEksilt()
    {
        Globals.ins.SetGlobals(GloabalResource.Wood, woodN);
        Globals.ins.SetGlobals(GloabalResource.Gold, goldN);
        Globals.ins.SetGlobals(GloabalResource.Stone, stoneN);
        Globals.ins.SetGlobals(GloabalResource.Iron, ironN);
    }
    static bool CompareResource(int needed, int globalVeri)
    {
        return needed <= globalVeri;
    }
}
