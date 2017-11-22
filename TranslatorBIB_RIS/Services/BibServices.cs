using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TranslatorBIB_RIS.Models;

namespace TranslatorBIB_RIS.Services
{
    public class BibServices
    {
        private static BibServices _instance;
        private List<BibContein> _BIBrecords;
        public List<Record> _records;
        private static RecordsServices _recordServices;

        public static BibServices Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BibServices();
                    _instance._BIBrecords = new List<BibContein>();
                    _recordServices = RecordsServices.Instance;
                    _instance._records = _recordServices.GetAll();
                }
                return _instance;
            }
        }

        public List<BibContein> ParseBib(string bib)
        {
            List<BibContein> bibModel = new List<BibContein>();

            for (int i = 0; i < bib.Length; i++)
            {

                if (bib[i] == '@')
                {
                    BibContein newBib = new BibContein();
                    String nowy = "";
                    int j = i + 1;
                    do//nazwa np conferenc
                    {
                        nowy += bib[j];
                        j += 1;
                    } while (bib[j] != '{');
                    // nazwy.Add(nowy);
                    newBib.bibType = nowy;


                    j += 1;
                    if (nowy != "Comment")
                    {
                        nowy = "";
                        do//klucz
                        {
                            nowy += bib[j];
                            j += 1;
                        } while (bib[j] != ',');
                        newBib.bibKey = nowy;

                        nowy = "";
                    }
                    else break;

                    nowy = "";
                    bool end = false;
                    do
                    {

                        if (bib[j] == '=')
                        {
                            int jL = j - 1;//tag
                            int jK = j + 3;//value
                            do
                            {


                                nowy = bib[jL] + nowy;
                                jL -= 1;
                            } while (bib[jL] != ',');
                            newBib.bibTags.Add(nowy);
                            nowy = "";
                            do
                            {

                                nowy += bib[jK];
                                jK += 1;

                            } while (bib[jK] != '}');


                            newBib.bibValues.Add(nowy);
                            nowy = "";

                            if (bib[jK + 4] == '}')
                                end = true;
                        }

                        j += 1;

                    } while (end != true);
                    i = j;
                    bibModel.Add(newBib);


                }


            }
            return bibModel;
        }
    }
}