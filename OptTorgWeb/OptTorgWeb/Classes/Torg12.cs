using Elfie.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.Util.Collections;
using NPOI.XSSF.UserModel;
using OptTorgWebDB.Models;

namespace OptTorgWeb.Classes
{
    public class Torg12
    {
        public static byte[] CreateExcellTorg12(Delivery d)
        {
            //ИМЕНОВАННЫЕ ЯЧЕЙКИ
            byte[] excelBytes = Properties.Resources.Torg12_blank;

            // 2. Загружаем файл в память через MemoryStream
            using (var stream = new MemoryStream(excelBytes))
            {
                List<ProductPart> ppList = ProductPart.GetProductPartForDelivery(d.IdDelivery);

                IWorkbook workbook;
                workbook = new HSSFWorkbook(stream); // для .xls

                // Получаем все именованные диапазоны
                IList<IName> namedRanges = workbook.GetAllNames();

                Buys b = ProductPart.GetProductPartForDelivery(d.IdDelivery)[0].Buy;
                Suppliers s = b.Supplier;
                String sData = $"{s.Company}; Адрес - {s.Adress}; Телефон - {s.Phone}; Факс - {s.Fax}; ИНН - {s.Inn}";

                Storages strg = d.Storage;
                String strgData = $"ООО \"ОптТорг\"; Адрес - {strg.Adress}; Телефон - {strg.Phone}; Факс - {strg.Fax}; ИНН - 7484247";

                DateTime thisDay = DateTime.Today;
                String date = thisDay.ToString("d");
                FeelNamedCellData(sData, "SupplierInfo", workbook);
                FeelNamedCellData("Отдел продаж", "SupplierPodrazdelenie", workbook);
                FeelNamedCellData(strgData, "Gruzopolouch", workbook);
                FeelNamedCellData(sData, "SupplierInfo3", workbook);
                FeelNamedCellData(strgData, "OurCompanyInfo2", workbook);
                FeelNamedCellData(d.Osnovanie, "Osnovanie", workbook);
                FeelNamedCellData(Convert.ToString(d.IdDelivery), "TorgId", workbook);
                FeelNamedCellData(date, "TorgDate", workbook);

                double divMassNett = (from pp in ppList
                                      select pp.Count * pp.Buy.Product.MassNetto).Sum();

                string[] parts = divMassNett.ToString().Split(',');
                parts[1] = parts[1].Substring(0, 2);
                FeelNamedCellData(NumToPropis.NumberToWord(Convert.ToDecimal(parts[0])) + " т. "
                    + NumToPropis.NumberToWord(Convert.ToDecimal(parts[1])) + " кг. "
                    , "MassNetto", workbook);

                double divMassBrutt = (from pp in ppList
                                       select pp.Count * pp.Buy.Product.MassPrutto).Sum();

                parts = divMassBrutt.ToString().Split(',');
                parts[1].Substring(0, 2);

                FeelNamedCellData(NumToPropis.NumberToWord(Convert.ToDecimal(parts[0])) + " т. "
                    + NumToPropis.NumberToWord(Convert.ToDecimal(parts[1])) + " кг. ", "MassBrutto", workbook);

                double totalCoast = (from pp in ppList
                                      select pp.Count * pp.Buy.Product.PricePerPiece).Sum();
                
                parts = totalCoast.ToString().Split(',');
                FeelNamedCellData(NumToPropis.NumberToWord(Convert.ToDecimal(parts[0])), "SummaPropis", workbook);
                
                if(parts.Count() == 2) 
                    FeelNamedCellData(NumToPropis.NumberToWord(Convert.ToDecimal(parts[1])), "SummaCop", workbook);
                else
                    FeelNamedCellData("ноль", "SummaCop", workbook);

                FeelNamedCellData(d.EmployeeAccept.Position.Name, "GruzPrinDoljnost", workbook);
                FeelNamedCellData(d.EmployeeAccept.Surname, "GruzPrinFIO", workbook);
                FeelNamedCellData(d.EmployeeReceive.Position.Name, "GruzGetDoljnost", workbook);
                FeelNamedCellData(d.EmployeeReceive.Surname, "GruzGetFIO", workbook);

                string[] dDate = d.Date.ToLongDateString().Split(" ");
                FeelNamedCellData(dDate[0], "DataChislo", workbook);
                FeelNamedCellData(dDate[1], "DataMesac", workbook);
                FeelNamedCellData(dDate[2], "DataGod", workbook);

                string dInfo = $"{ppList[0].Buy.Supplier.Company}, ООО \"ОптТорг\", {d.EmployeeAccept.Surname} {d.EmployeeAccept.Name} {d.EmployeeAccept.Position.Name}";

                FeelNamedCellData(dInfo, "DoverInfo1", workbook);

                FeelNamedCellData(d.NomerDoverennosti, "NomerDoverennosti", workbook);
                decimal mest = (from pp in ppList
                               select pp.Count).Sum();
                FeelNamedCellData(NumToPropis.NumberToWord(mest), "VsegoMest", workbook);

                FeelNamedCellData(NumToPropis.NumberToWord(ppList.Count()), "ZapisiCount", workbook);

                ISheet sheet = workbook.GetSheet("стр1");
                List<int> gaps = new List<int>() {3,16,5,5,5,5,5,5,6,9,7,4,7,9 };
                
                for(int i=0;i< ppList.Count();i++)
                {
                    Pair<int, int> rowStart = new Pair<int, int>(30+i, 0);
                    ProductPart pp = ppList[i];

                    //Номер по порядку
                    FillMergedCell(sheet, rowStart.First, rowStart.Second, i+1);
                    //Наименование
                    rowStart.Second = rowStart.Second + gaps[0] + 1;
                    FillMergedCell(sheet, rowStart.First, rowStart.Second, pp.Buy.Product.ProductName);
                    //код
                    rowStart.Second = rowStart.Second + gaps[1];
                    FillMergedCell(sheet, rowStart.First, rowStart.Second, pp.Buy.Product.ProductCode);
                    //наименование ед. измерения
                    rowStart.Second = rowStart.Second + gaps[2];
                    FillMergedCell(sheet, rowStart.First, rowStart.Second, pp.Buy.Product.Mu.Name);
                    //код океи
                    rowStart.Second = rowStart.Second + gaps[3];
                    FillMergedCell(sheet, rowStart.First, rowStart.Second, pp.Buy.Product.Mu.OkeiCode);
                    //вид упаковки
                    rowStart.Second = rowStart.Second + gaps[4];
                    FillMergedCell(sheet, rowStart.First, rowStart.Second, pp.Buy.Product.Packing.Type);
                    //в одном месте
                    rowStart.Second = rowStart.Second + gaps[5];
                    FillMergedCell(sheet, rowStart.First, rowStart.Second, pp.Buy.Product.CountPerPlase);
                    //мест штук
                    rowStart.Second = rowStart.Second + gaps[6];
                    FillMergedCell(sheet, rowStart.First, rowStart.Second, pp.Count);
                    //масса брутто
                    rowStart.Second = rowStart.Second + gaps[7];
                    FillMergedCell(sheet, rowStart.First, rowStart.Second, pp.Buy.Product.MassPrutto);
                    //масса нетто количество
                    rowStart.Second = rowStart.Second + gaps[8];
                    FillMergedCell(sheet, rowStart.First, rowStart.Second, pp.Buy.Product.MassNetto);
                    //цена
                    rowStart.Second = rowStart.Second + gaps[9];
                    FillMergedCell(sheet, rowStart.First, rowStart.Second, pp.Buy.Product.PricePerPiece);
                    //сумма без ндс
                    double noNds = pp.Buy.Product.PricePerPieceNoNds ?? 0.0;
                    rowStart.Second = rowStart.Second + gaps[10];
                    FillMergedCell(sheet, rowStart.First, rowStart.Second, noNds * pp.Count);
                    //ставка %
                    double nds = pp.Buy.Product.Nds ?? 0.0;
                    rowStart.Second = rowStart.Second + gaps[11];
                    FillMergedCell(sheet, rowStart.First, rowStart.Second, nds*100);
                    //ндс сумма
                    rowStart.Second = rowStart.Second + gaps[12];
                    FillMergedCell(sheet, rowStart.First, rowStart.Second, pp.Buy.Product.PricePerPiece * pp.Count - noNds * pp.Count);
                    //сумма с учетом ндс
                    rowStart.Second = rowStart.Second + gaps[13];
                    FillMergedCell(sheet, rowStart.First, rowStart.Second, pp.Buy.Product.PricePerPiece*pp.Count);
                }

                // Сохраняем изменения
                //using (FileStream outFs = new FileStream(_outputPath, FileMode.Create, FileAccess.Write))
                //{
                //    workbook.Write(outFs);
                //}

                return excelBytes;
            }
        }

        private static bool IsXlsxFormat(byte[] excelBytes)
        {
            throw new NotImplementedException();
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
