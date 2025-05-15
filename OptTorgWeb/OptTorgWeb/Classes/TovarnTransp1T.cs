using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using OptTorgWebDB.Models;

namespace OptTorgWeb.Classes
{
    public class TovarnTransp1T
    {
        public static byte[] CreateExcell1T(Sending sending)
        {

            byte[] excelBytes = Properties.Resources._1Tblank;

            //ИМЕНОВАННЫЕ ЯЧЕЙКИ
            using (var stream = new MemoryStream(excelBytes))
            {
                List<CargoInfo> crgInf = CargoInfo.GetCargoInfoForSending(sending.IdSending);
                Sales sale = Sales.GetSalesByIdNavigation(crgInf[0].SaleId);

                IWorkbook workbook;
                workbook = new XSSFWorkbook(stream); // для .xlsx

                //Шапка
                FeelNamedCellData(sending.IdSending, "NomerNakladnoy", workbook);
                
                string[] dDate = DateTime.Today.ToLongDateString().Split(" ");
                FeelNamedCellData(dDate[0], "DataDay", workbook);
                FeelNamedCellData(dDate[1], "DataMonth", workbook);
                FeelNamedCellData(dDate[2], "DataY", workbook);


                //Доверенность
                FeelNamedCellData(sending.NomerDoverenosti, "NomerDover", workbook);
                
                string[] dDate1 = sending.DataDoverenosti.Value.ToLongDateString().Split(" ");
                FeelNamedCellData(dDate1[0], "DoverennostDay", workbook);
                FeelNamedCellData(dDate1[1], "DoverennostMonth", workbook);
                FeelNamedCellData(dDate1[2], "DoverennostY", workbook);

                string dInfo = $"{sending.Customer.Company} (ИНН {sending.Customer.Inn}), " +
                    $"в лице {sending.Customer.Surname}  {sending.Customer.Name}, действующей на основании Устава," +
                    $"получить товарно - материальные ценности по накладной {sending.NomerDoverenosti}";
                FeelNamedCellData(dInfo, "KemVidanaDover", workbook);

                FeelList1OrgData(workbook,
                    sale.Customer,sending.Storage,
                    sending.SeOtpuskRazreshil.EmployeeNavigation,sending.SeOtpuskProizvel.EmployeeNavigation,
                    sending.GlavBuh,sending.Driver);

                FeelList1ProdTable(workbook,crgInf);

                FeelList2OrgData(workbook, sending,sending.Driver,sending.Transport,sending.Storage);

                FeelCargoInfoList2(workbook, crgInf,sending.Driver);

                FeelPogruzRazgruzList2(workbook); 

                FeelProchieSvedeniaList2(workbook,sending);

                FeelRascenkiList2(workbook, sending);

                // Сохраняем изменения
                //using (FileStream outFs = new FileStream(_outputPath, FileMode.Create, FileAccess.Write))
                //{
                //    workbook.Write(outFs);
                //}

                return excelBytes;
            }
        }

        private static void FeelList2OrgData(IWorkbook workbook,Sending s,Drivers driver, Transport transport,Storages storage)
        {
            string[] dDate1 = s.DateArrivalUnloading.Value.ToLongDateString().Split(" ");
            FeelNamedCellData(dDate1[0], "SendingDateDay", workbook);
            FeelNamedCellData(dDate1[1], "SendingDateMon", workbook);
            FeelNamedCellData(dDate1[2], "SendingDateY", workbook);

            String gruzootprav = $"ООО \"ОптТорг\"; Адрес - {storage.Adress}; Телефон - {storage.Phone}; Факс - {storage.Fax}; ИНН - 7484247";
            String gruzopoluch = $"{s.Customer.Company}; Адрес - {s.Customer.Adress}; Телефон - {s.Customer.Phone}; " +
                $"Факс - {s.Customer.Fax}; ИНН - {s.Customer.Inn}";

            FeelNamedCellData(gruzootprav, "Gruzootprav2", workbook);

            FeelNamedCellData(transport.Name, "TransportMarka", workbook);
            FeelNamedCellData(transport.GosNomer, "GosNomerAvto", workbook);

            FeelNamedCellData(gruzopoluch, "Gruzopoluch2", workbook);

            String driverFIO = $"{driver.SeNavigation.EmployeeNavigation.Surname} " +
                $"{driver.SeNavigation.EmployeeNavigation.Name}" +
                $"{driver.SeNavigation.EmployeeNavigation.Patronomic}";

            FeelNamedCellData(driverFIO, "DriverFIO", workbook);
            FeelNamedCellData(driver.License, "UdostoverenieNomer", workbook);

            FeelNamedCellData(storage.Adress, "StorageAdress", workbook);
            FeelNamedCellData(s.Customer.Adress, "RazgruzkaAdress", workbook);
        }

        private static void FeelCargoInfoList2(IWorkbook workbook, List<CargoInfo> crgiList,Drivers driver)
        {
            List<Products> prods = new List<Products>();

            foreach (CargoInfo ci in crgiList)
            {
                prods.Add(Products.GetProductsById(ci.Sale.ProductId));
            }

            //Заполнение таблицы
            List<int> gaps = new List<int>() { 31, 38, 18, 16, 33, 11, 13, 11, 13};
            ISheet sheet = workbook.GetSheet("стр2");

            for (int i = 0; i < crgiList.Count(); i++)
            {
                Pair<int, int> rowStart = new Pair<int, int>(25 + i, 4);
                Products prod = prods[i];

                //1
                FillMergedCell(sheet, rowStart.First, rowStart.Second, prod.ProductName);

                //2
                rowStart.Second = rowStart.Second + gaps[0] + 1;
                FillMergedCell(sheet, rowStart.First, rowStart.Second, prod.IdProduct);

                //3
                rowStart.Second = rowStart.Second + gaps[1];
                FillMergedCell(sheet, rowStart.First, rowStart.Second, prod.Packing.Type);

                //4
                rowStart.Second = rowStart.Second + gaps[2];
                FillMergedCell(sheet, rowStart.First, rowStart.Second, crgiList[i].Count*prod.CountPerPlase);

                //5
                rowStart.Second = rowStart.Second + gaps[3];
                FillMergedCell(sheet, rowStart.First, rowStart.Second, "статический");

                //6
                rowStart.Second = rowStart.Second + gaps[4];
                FillMergedCell(sheet, rowStart.First, rowStart.Second, prod.IdProduct);

                //7
                rowStart.Second = rowStart.Second + gaps[5];
                FillMergedCell(sheet, rowStart.First, rowStart.Second, i);

                //8
                rowStart.Second = rowStart.Second + gaps[6];
                FillMergedCell(sheet, rowStart.First, rowStart.Second, "1");

                //9
                rowStart.Second = rowStart.Second + gaps[7];
                FillMergedCell(sheet, rowStart.First, rowStart.Second, crgiList[i].Count * prod.MassPrutto);
            }
            
            int vsegoMest = (from pp in crgiList
                             select pp.Count * pp.Sale.Product.CountPerPlase).Sum();
            FeelNamedCellData(NumToPropis.NumberToWord(vsegoMest), "MestVsego", workbook);

            double massBrutto = (from pp in crgiList
                                select pp.Count * pp.Sale.Product.MassPrutto).Sum();
            string[] str1 = massBrutto.ToString().Split(',');
            string m ="";
            
            if ((int)massBrutto != 0)
            {
                m = NumToPropis.NumberToWord(Convert.ToDecimal(str1[0])) + " т. ";

                if (str1.Count() == 2)
                {
                    m += NumToPropis.NumberToWord(Convert.ToDecimal(str1[1])) + " кг. ";
                }
                else
                {
                    m+= " ноль кг.";
                }
            }
            else
            {
                m +=NumToPropis.NumberToWord(Convert.ToDecimal(str1[1]));
            }

            FeelNamedCellData(m, "MassBruttoVsego3", workbook);
            FeelNamedCellData(m, "MassaBrutto2", workbook);

            FeelNamedCellData((decimal)massBrutto, "MassBruttoIto2", workbook);

            string doljnost = driver.SeNavigation.EmployeeNavigation.Position.Name;
            string fio= driver.SeNavigation.EmployeeNavigation.Surname;

            FeelNamedCellData(doljnost, "SdalDoljnost", workbook);
            FeelNamedCellData(fio, "SdalFio", workbook);

            FeelNamedCellData(fio, "VoditelPrinalFio", workbook); 

            FeelNamedCellData(fio, "VoditelSdalFIO", workbook); 

        }

        private static void FeelPogruzRazgruzList2(IWorkbook workbook)
        {

        }

        private static void FeelRascenkiList2(IWorkbook workbook,Sending s)
        {
            ISheet sheet = workbook.GetSheet("стр2");

            Pricing prsng = s.Pricing;
            double vsego1 = 0.0, vsego2 = 0.0, vsego3 = 0.0;
            int col = 18;
            int row = 61;
            
            List<CargoInfo> crgInf = CargoInfo.GetCargoInfoForSending(s.IdSending);
            List<Products> prods = new List<Products>();

            foreach (CargoInfo ci in crgInf)
            {
                prods.Add(Products.GetProductsById(ci.Sale.ProductId));
            }

            double d1 = s.Distance1TypeRoad ?? 0.0;
            double d2 = s.Distance2TypeRoad ?? 0.0;
            double d3 = s.Distance3TypeRoad ?? 0.0;
            double d4 = s.DistanceInCity ?? 0.0;

            double tonn = (from pp in crgInf
                           select pp.Count * pp.Sale.Product.MassNetto).Sum();

            double tonnkm = tonn + d1+d2*d3+d4;
            //33
            vsego1 += prsng.ForTonnes;
            vsego2 += tonn;
            vsego3 += prsng.ForTonnes* tonn;

            FillMergedCell(sheet, row, col, prsng.ForTonnes);
            FillMergedCell(sheet, row-1, col, tonn);
            FillMergedCell(sheet, row+1, col, prsng.ForTonnes * tonn);

            //34
            col += 8;
            vsego1 += prsng.ForTonnesKm;
            vsego2 += tonnkm;
            vsego3 += prsng.ForTonnesKm * tonnkm;

            FillMergedCell(sheet, row-1, col, tonnkm);
            FillMergedCell(sheet, row+1, col, prsng.ForTonnesKm * tonnkm);
            FillMergedCell(sheet, row, col, prsng.ForTonnesKm);

            //35
            col += 9;
            vsego1 += prsng.PogruzRazgruz;
            vsego2 += tonn*2.0;
            vsego3 += prsng.PogruzRazgruz* tonn * 2.0;

            FillMergedCell(sheet, row-1, col, tonn * 2.0);
            FillMergedCell(sheet, row+1, col, prsng.PogruzRazgruz * tonn * 2.0);
            FillMergedCell(sheet, row, col, prsng.PogruzRazgruz);

            //36
            col += 14;
            vsego1 += prsng.NedogruzAvto;
            vsego2 += s.Transport.Tonnage - Sending.GetSendingWeight(s.IdSending);
            vsego3 += prsng.NedogruzAvto*(s.Transport.Tonnage - Sending.GetSendingWeight(s.IdSending));

            FillMergedCell(sheet, row-1, col, s.Transport.Tonnage - Sending.GetSendingWeight(s.IdSending));
            FillMergedCell(sheet, row+1, col, prsng.NedogruzAvto * (s.Transport.Tonnage - Sending.GetSendingWeight(s.IdSending)));
            FillMergedCell(sheet, row, col, prsng.NedogruzAvto);

            //37
            col += 14;
            vsego1 += prsng.Expedirovanie;
            double exp = s.Expedirovanie ?? 0.0;
            vsego2 += exp;
            vsego3 += exp* prsng.Expedirovanie;

            FillMergedCell(sheet, row-1, col, exp);
            FillMergedCell(sheet, row+1, col, exp * prsng.Expedirovanie);
            FillMergedCell(sheet, row, col, prsng.Expedirovanie);

            //38
            DateTimeOffset dto1 =  s.DowntimeLoading ?? DateTimeOffset.MinValue;
            double h1 = dto1.TimeOfDay.TotalHours;

            col += 11;
            vsego1 += prsng.DowntimeLoading; 
            vsego2 += h1;
            vsego3 += prsng.DowntimeLoading * h1;

            FillMergedCell(sheet, row-1, col, h1);
            FillMergedCell(sheet, row+1, col, prsng.DowntimeLoading * h1);
            FillMergedCell(sheet, row, col, prsng.DowntimeLoading);

            //39
            col += 11;
            DateTimeOffset dto2 = s.DowntimeUnloading ?? DateTimeOffset.MinValue;
            double h2 = dto1.TimeOfDay.TotalHours;

            vsego1 += prsng.DowntimeUnloading;
            vsego2 += h2;
            vsego3 += prsng.DowntimeUnloading * h2;

            FillMergedCell(sheet, row-1, col, h2);
            FillMergedCell(sheet, row+1, col, prsng.DowntimeUnloading * h2);
            FillMergedCell(sheet, row, col, prsng.DowntimeUnloading);

            //40
            col += 10;
            vsego1 += prsng.ZaSrochnost;
            double zaSrocnost = s.ZaSrochnost ?? 0.0;
            vsego2 += zaSrocnost;
            vsego3 += prsng.ZaSrochnost* zaSrocnost;

            FillMergedCell(sheet, row-1, col, zaSrocnost);
            FillMergedCell(sheet, row+1, col, prsng.ZaSrochnost * zaSrocnost);
            FillMergedCell(sheet, row, col, prsng.ZaSrochnost);

            //41
            col += 9;
            vsego1 += prsng.ZaSpecTransport;
            double zaSpcTrnsprt = s.ZaSpecTransport ?? 0.0;
            vsego2 += zaSpcTrnsprt;
            vsego3 += prsng.ZaSpecTransport* zaSpcTrnsprt;

            FillMergedCell(sheet, row-1, col, zaSpcTrnsprt);
            FillMergedCell(sheet, row+1, col, prsng.ZaSpecTransport * zaSpcTrnsprt);
            FillMergedCell(sheet, row, col, prsng.ZaSpecTransport);

            //42
            col += 11;
            vsego1 += prsng.Other;
            double othter = s.OtherExtraPay ?? 0.0;
            vsego2 += othter;
            vsego3 += othter* prsng.Other;

            FillMergedCell(sheet, row-1, col, othter);
            FillMergedCell(sheet, row+1, col, othter * prsng.Other);
            FillMergedCell(sheet, row, col, prsng.Other);

            //43
            col += 9;
            FillMergedCell(sheet, row-1, col, vsego2);
            FillMergedCell(sheet, row+1, col, vsego3);
            FillMergedCell(sheet, row, col, vsego1);

        }

        private static void FeelProchieSvedeniaList2(IWorkbook workbook,Sending s)
        {
            ISheet sheet = workbook.GetSheet("стр2");

            int row = 55;
            int col = 0;

            //20
            double d1 = s.Distance1TypeRoad ?? 0.0;
            double d2 = s.Distance2TypeRoad ?? 0.0;
            double d3 = s.Distance3TypeRoad ?? 0.0;
            double d4 = s.DistanceInCity ?? 0.0;

            FillMergedCell(sheet, row, col,d1+d2+d3+d4);

            //21
            col += 8+1;
            FillMergedCell(sheet, row, col, d4);
            
            //22
            col += 8;
            FillMergedCell(sheet, row, col, d1);
            
            //23
            col += 8;
            FillMergedCell(sheet, row, col, d2);
           
            //24
            col += 8;
            FillMergedCell(sheet, row, col, d3);

            //25
            col += 8;
            FillMergedCell(sheet, row, col, s.CodExpedir);
            
            //26
            col += 9;
            double sc = s.SClientaZaUslugi ?? 0.0;
            FillMergedCell(sheet, row, col, sc);
            
            //27
            col += 11;
            double v = s.VoditeluZaUslugi ?? 0.0;
            FillMergedCell(sheet, row, col, v);

            //28
            col += 14;
            double shtaf = s.Shtraph ?? 0.0;
            FillMergedCell(sheet, row, col, shtaf);
            
            //29
            col += 18;
            double rd = s.RascenkaVoditelu ?? 0.0;
            FillMergedCell(sheet, row, col, rd);

            //30
            col += 13;
            double mt = s.MainTarriffs ?? 0.0;
            FillMergedCell(sheet, row, col, mt);
        }

        private static void FeelList1ProdTable(IWorkbook workbook, List<CargoInfo> crgiList)
        {
            List<Products> prods = new List<Products>();

            foreach (CargoInfo ci in crgiList)
            {
                prods.Add(Products.GetProductsById(ci.Sale.ProductId));
            }

            double massNetto = (from pp in crgiList
                                select pp.Count * pp.Sale.Product.MassNetto).Sum();

            int vsegoMest = (from pp in crgiList
                             select pp.Count * pp.Sale.Product.CountPerPlase).Sum();

            int vsegoNaimenovani = crgiList.Count;

            double vsegoSumma = (from pp in crgiList
                                 select pp.Count * pp.Sale.Product.PricePerPiece).Sum();
            //Итоги
            string[] str1 = massNetto.ToString().Split(',');
            if((int)massNetto != 0)
            {
                FeelNamedCellData(NumToPropis.NumberToWord(Convert.ToDecimal(str1[0])) + " т. ", "MassNetto", workbook);
                FeelNamedCellData(NumToPropis.NumberToWord(Convert.ToDecimal(str1[0])) + " т. ", "MassNetto2", workbook);

                if (str1.Count() == 2)
                {
                    FeelNamedCellData(NumToPropis.NumberToWord(Convert.ToDecimal(str1[1])) + " кг. ", "MassNetto2", workbook);
                    FeelNamedCellData(NumToPropis.NumberToWord(Convert.ToDecimal(str1[1]))+ " кг. ", "MassNetto", workbook);
                }
                else
                {
                    FeelNamedCellData("ноль кг.", "MassNetto2", workbook);
                    FeelNamedCellData("ноль кг.", "MassNetto", workbook);
                }
            }
            else
            {
                FeelNamedCellData(NumToPropis.NumberToWord(Convert.ToDecimal(str1[0])) + " кг. ", "MassNetto2", workbook);
                FeelNamedCellData(NumToPropis.NumberToWord(Convert.ToDecimal(str1[0])) + " кг. ", "MassNetto", workbook);
            }

            FeelNamedCellData((decimal)massNetto, "TonnChislo", workbook);
            FeelNamedCellData((decimal)massNetto, "TonnChislo2", workbook);

            FeelNamedCellData(NumToPropis.NumberToWord(vsegoMest), "VsegoMest", workbook);
            FeelNamedCellData(NumToPropis.NumberToWord(vsegoNaimenovani), "VsegoNaimenovani", workbook);

            string[] str3 = vsegoSumma.ToString().Split(',');
            FeelNamedCellData(NumToPropis.NumberToWord(Convert.ToDecimal(str3[0])), "VsegoSummaRub", workbook);
            if (str3.Count() == 2)
                FeelNamedCellData(NumToPropis.NumberToWord(Convert.ToDecimal(str3[1])), "VsegoSummaCop", workbook);
            else
                FeelNamedCellData("ноль", "VsegoSummaCop", workbook);

            //Заполнение таблицы
            List<int> gaps = new List<int>() { 11, 20, 13, 12, 16, 39, 11, 11, 11, 14, 11, 18};
            ISheet sheet = workbook.GetSheet("стр1");

            for (int i = 0; i < crgiList.Count(); i++)
            {
                Pair<int, int> rowStart = new Pair<int, int>(16 + i, 0);
                Products prod = prods[i];

                //1
                FillMergedCell(sheet, rowStart.First, rowStart.Second, prod.IdProduct);

                //2
                rowStart.Second = rowStart.Second + gaps[0] + 1;
                FillMergedCell(sheet, rowStart.First, rowStart.Second, prod.IdProduct);

                //3
                rowStart.Second = rowStart.Second + gaps[1];
                FillMergedCell(sheet, rowStart.First, rowStart.Second, prod.ProductCode);

                //4
                rowStart.Second = rowStart.Second + gaps[2];
                FillMergedCell(sheet, rowStart.First, rowStart.Second, crgiList[i].Count);

                //5
                rowStart.Second = rowStart.Second + gaps[3];
                FillMergedCell(sheet, rowStart.First, rowStart.Second, prod.PricePerPiece);

                //6
                rowStart.Second = rowStart.Second + gaps[4];
                FillMergedCell(sheet, rowStart.First, rowStart.Second, $"{prod.ProductName}, {prod.Sort.SortName}");

                //7
                rowStart.Second = rowStart.Second + gaps[5];
                FillMergedCell(sheet, rowStart.First, rowStart.Second, prod.Mu.Name);

                //8
                rowStart.Second = rowStart.Second + gaps[6];
                FillMergedCell(sheet, rowStart.First, rowStart.Second, prod.Packing.Type);
                
                //9
                rowStart.Second = rowStart.Second + gaps[7];
                FillMergedCell(sheet, rowStart.First, rowStart.Second, prod.CountPerPlase);

                //10
                rowStart.Second = rowStart.Second + gaps[8];
                FillMergedCell(sheet, rowStart.First, rowStart.Second, prod.MassPrutto * crgiList[i].Count);
                
                //11
                rowStart.Second = rowStart.Second + gaps[9];
                FillMergedCell(sheet, rowStart.First, rowStart.Second, prod.PricePerPiece * crgiList[i].Count);

                //12
                rowStart.Second = rowStart.Second + gaps[10];
                FillMergedCell(sheet, rowStart.First, rowStart.Second, i+1);
            }
        }

        private static void FeelList1OrgData(IWorkbook workbook,
            Customers cus, Storages strg,
            Employees otpuskRazreshil, Employees otpuskProizvel,
            Employees glavBuh, Drivers perevozchik)
        {
            String gruzootprav = $"ООО \"ОптТорг\"; Адрес - {strg.Adress}; Телефон - {strg.Phone}; Факс - {strg.Fax}; ИНН - 7484247";
            String gruzopoluch = $"{cus.Company}; Адрес - {cus.Adress}; Телефон - {cus.Phone}; Факс - {cus.Fax}; ИНН - {cus.Inn}";

            //Шапка
            FeelNamedCellData(gruzootprav, "Gruzootprav", workbook);
            FeelNamedCellData(gruzopoluch, "Gruzopoluch1", workbook);
            FeelNamedCellData(gruzopoluch, "Platilshik", workbook);

            //Кто куда
            FeelNamedCellData(otpuskRazreshil.Position.Name, "OtpustilDoljnost", workbook);
            FeelNamedCellData(otpuskRazreshil.Surname, "OtpustilFIO", workbook);
            
            FeelNamedCellData(glavBuh.Surname, "GlavBuh", workbook);

            FeelNamedCellData(otpuskProizvel.Position.Name, "ProizvelDoljnost", workbook);
            FeelNamedCellData(otpuskProizvel.Surname, "ProizvelFio", workbook);
            
            FeelNamedCellData(perevozchik.SeNavigation.EmployeeNavigation.Position.Name, "PerevozchikDolznost", workbook);
            FeelNamedCellData(perevozchik.SeNavigation.EmployeeNavigation.Surname, "PerevozchikFio", workbook);
        }

        private static void FeelNamedCellData(decimal d, string cellName, IWorkbook workbook)
        {
            IList<IName> namedRanges = workbook.GetAllNames();
            IName namedCell = namedRanges.FirstOrDefault(n => n.NameName == cellName);

            // Получаем область действия имени (лист и диапазон)
            ISheet sheet = workbook.GetSheet(namedCell.SheetName);
            CellRangeAddress range = CellRangeAddress.ValueOf(namedCell.RefersToFormula);

            // Записываем данные в первую ячейку диапазона
            IRow row = sheet.GetRow(range.FirstRow) ?? sheet.CreateRow(range.FirstRow);
            ICell cell = row.GetCell(range.FirstColumn) ?? row.CreateCell(range.FirstColumn);

            cell.SetCellValue((double)d);
        }

        private static void FeelNamedCellData(string data, string cellName, IWorkbook workbook)
        {
            IList<IName> namedRanges = workbook.GetAllNames();
            IName namedCell = namedRanges.FirstOrDefault(n => n.NameName == cellName);

            // Получаем область действия имени (лист и диапазон)
            ISheet sheet = workbook.GetSheet(namedCell.SheetName);
            CellRangeAddress range = CellRangeAddress.ValueOf(namedCell.RefersToFormula);

            // Записываем данные в первую ячейку диапазона
            IRow row = sheet.GetRow(range.FirstRow) ?? sheet.CreateRow(range.FirstRow);
            ICell cell = row.GetCell(range.FirstColumn) ?? row.CreateCell(range.FirstColumn);

            cell.SetCellValue(data);
        }

        private static void FillMergedCell(ISheet sheet, int rowIndex, int columnIndex, int value)
        {
            // Находим объединённую область для этой ячейки (если есть)
            CellRangeAddress mergedRegion = sheet.MergedRegions
                .Cast<CellRangeAddress>()
                .FirstOrDefault(r =>
                    r.FirstRow <= rowIndex && rowIndex <= r.LastRow &&
                    r.FirstColumn <= columnIndex && columnIndex <= r.LastColumn);

            if (mergedRegion != null)
            {
                // Заполняем только первую ячейку объединённой области
                IRow row = sheet.GetRow(mergedRegion.FirstRow) ?? sheet.CreateRow(mergedRegion.FirstRow);
                ICell cell = row.GetCell(mergedRegion.FirstColumn) ?? row.CreateCell(mergedRegion.FirstColumn);
                cell.SetCellValue(value);
            }
            else
            {
                // Обычная ячейка
                IRow row = sheet.GetRow(rowIndex) ?? sheet.CreateRow(rowIndex);
                ICell cell = row.GetCell(columnIndex) ?? row.CreateCell(columnIndex);
                cell.SetCellValue(value);
            }
        }

        private static void FillMergedCell(ISheet sheet, int rowIndex, int columnIndex, double value)
        {
            // Находим объединённую область для этой ячейки (если есть)
            CellRangeAddress mergedRegion = sheet.MergedRegions
                .Cast<CellRangeAddress>()
                .FirstOrDefault(r =>
                    r.FirstRow <= rowIndex && rowIndex <= r.LastRow &&
                    r.FirstColumn <= columnIndex && columnIndex <= r.LastColumn);

            if (mergedRegion != null)
            {
                // Заполняем только первую ячейку объединённой области
                IRow row = sheet.GetRow(mergedRegion.FirstRow) ?? sheet.CreateRow(mergedRegion.FirstRow);
                ICell cell = row.GetCell(mergedRegion.FirstColumn) ?? row.CreateCell(mergedRegion.FirstColumn);
                cell.SetCellValue(value);
            }
            else
            {
                // Обычная ячейка
                IRow row = sheet.GetRow(rowIndex) ?? sheet.CreateRow(rowIndex);
                ICell cell = row.GetCell(columnIndex) ?? row.CreateCell(columnIndex);
                cell.SetCellValue(value);
            }
        }

        private static void FillMergedCell(ISheet sheet, int rowIndex, int columnIndex, string value)
        {
            // Находим объединённую область для этой ячейки (если есть)
            CellRangeAddress mergedRegion = sheet.MergedRegions
                .Cast<CellRangeAddress>()
                .FirstOrDefault(r =>
                    r.FirstRow <= rowIndex && rowIndex <= r.LastRow &&
                    r.FirstColumn <= columnIndex && columnIndex <= r.LastColumn);

            if (mergedRegion != null)
            {
                // Заполняем только первую ячейку объединённой области
                IRow row = sheet.GetRow(mergedRegion.FirstRow) ?? sheet.CreateRow(mergedRegion.FirstRow);
                ICell cell = row.GetCell(mergedRegion.FirstColumn) ?? row.CreateCell(mergedRegion.FirstColumn);
                cell.SetCellValue(value);
            }
            else
            {
                // Обычная ячейка
                IRow row = sheet.GetRow(rowIndex) ?? sheet.CreateRow(rowIndex);
                ICell cell = row.GetCell(columnIndex) ?? row.CreateCell(columnIndex);
                cell.SetCellValue(value);
            }
        }

        private class Pair<T, U>
        {
            public Pair()
            {
            }

            public Pair(T first, U second)
            {
                this.First = first;
                this.Second = second;
            }

            public T First { get; set; }
            public U Second { get; set; }
        };
    }
}

