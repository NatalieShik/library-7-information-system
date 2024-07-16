using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApplicationLib.Models;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using System.IO;
using System.Data.Entity.SqlServer;
using System.Data.Entity;
using DocumentFormat.OpenXml.Wordprocessing;

namespace WebApplicationLib.Controllers
{
    public class HomeController : Controller
    {
        private LibrarySystemEntities db;

        public HomeController()
        {
            db = new LibrarySystemEntities();
        }

        public ActionResult Index()
        {
            return View(GetTopBooksInfo());
        }

        private List<BookInfo> GetTopBooksInfo()
        {
            var topBooks = db.Register_of_copies
            .GroupBy(rc => new { rc.Library_catalog_Id, rc.Library_catalog.Title })
            .Select(g => new BookInfo
            {
                Title = g.Key.Title,
                TotalCount = g.Count()
            })
            .OrderByDescending(g => g.TotalCount)
            .Take(3)
            .ToList();
            return topBooks;
        }

        private List<WorkersInfo> GetWorkersInfo()
        {
            List<WorkersInfo> workers = (from worker in db.Workers
                                         join jobTitle in db.Job_titles on worker.Job_title_Id equals jobTitle.Id
                                         group new { worker, jobTitle } by new { jobTitle.Title, worker.Name, worker.Surname, worker.Patronymic } into grouped
                                         select new WorkersInfo
                                         {
                                             Title = grouped.Key.Title,
                                             Name = grouped.Key.Name,
                                             Surname = grouped.Key.Surname,
                                             Patronymic = grouped.Key.Patronymic
                                         }).ToList();
            return workers;
        }

        private ReadersInfo GetReadersInfo()
        {
            ReadersInfo info = new ReadersInfo();
            info.TotalReaders = db.Register_of_copies
            .Where(rc => rc.Issued_to != null && SqlFunctions.DatePart("year", rc.When_issued) == DateTime.Now.Year)
            .Select(rc => rc.Issued_to)
            .Distinct()
            .Count();

            info.Visits = db.Register_of_copies
            .Where(rc => SqlFunctions.DatePart("year", rc.When_issued) == DateTime.Now.Year)
            .Count();

            return info;
        }

        private List<string> GetBooksInfo()
        {
            List<string> distinctTitles = db.Register_of_copies
                .Join(db.Library_catalog, rc => rc.Library_catalog_Id, lc => lc.Id, (rc, lc) => new { rc, lc })
                .Where(joined => DbFunctions.DiffYears(joined.rc.When_issued, DateTime.Now) == 0)
                .Select(joined => joined.lc.Title)
                .Distinct()
                .ToList();

            return distinctTitles;
        }

        public ActionResult GenerateDocx()
        {
            List<string> books = GetBooksInfo();
            List<WorkersInfo> workers = GetWorkersInfo();
            using (MemoryStream mem = new MemoryStream())
            {
                using (WordprocessingDocument doc = WordprocessingDocument.Create(mem, WordprocessingDocumentType.Document))
                {
                    MainDocumentPart mainPart = doc.AddMainDocumentPart();
                    mainPart.Document = new Document();
                    Body body = new Body();

                    // Заголовок документа
                    Paragraph title = new Paragraph(new Run(new Text("Годовой отчет по работе библиотеки №7")));
                    title.ParagraphProperties = new ParagraphProperties(new Justification() { Val = JustificationValues.Center });
                    body.Append(title);

                    title = new Paragraph(new Run(new Text("Материально-техническая база")));
                    title.ParagraphProperties = new ParagraphProperties(new Justification() { Val = JustificationValues.Center });
                    body.Append(title);

                    Paragraph text = new Paragraph(new Run(new Text("1. Книжные фонды:")));
                    body.Append(text);
                    text = new Paragraph(new Run(new Text("- Книги различных жанров и тематик;")));
                    text.ParagraphProperties = new ParagraphProperties(new Indentation() { Left = "720" });
                    body.Append(text);
                    text = new Paragraph(new Run(new Text("- Учебники;")));
                    text.ParagraphProperties = new ParagraphProperties(new Indentation() { Left = "720" });
                    body.Append(text);
                    text = new Paragraph(new Run(new Text("- Энциклопедии;")));
                    text.ParagraphProperties = new ParagraphProperties(new Indentation() { Left = "720" });
                    body.Append(text);
                    text = new Paragraph(new Run(new Text("- Журналы и периодические издания;")));
                    text.ParagraphProperties = new ParagraphProperties(new Indentation() { Left = "720" });
                    body.Append(text);
                    text = new Paragraph(new Run(new Text("- Аудио и видеоматериалы.")));
                    text.ParagraphProperties = new ParagraphProperties(new Indentation() { Left = "720" });
                    body.Append(text);
                    text = new Paragraph(new Run(new Text("2. Электронные ресурсы:")));
                    body.Append(text);
                    text = new Paragraph(new Run(new Text("- Доступ к онлайн-базам данных и электронным журналам;")));
                    text.ParagraphProperties = new ParagraphProperties(new Indentation() { Left = "720" });
                    body.Append(text);
                    text = new Paragraph(new Run(new Text("- Компьютеры и интернет для посетителей.")));
                    text.ParagraphProperties = new ParagraphProperties(new Indentation() { Left = "720" });
                    body.Append(text);
                    text = new Paragraph(new Run(new Text("3. Оборудование:")));
                    body.Append(text);
                    text = new Paragraph(new Run(new Text("- Компьютеры для библиотечного персонала;")));
                    text.ParagraphProperties = new ParagraphProperties(new Indentation() { Left = "720" });
                    body.Append(text);
                    text = new Paragraph(new Run(new Text("- Принтеры и сканеры;")));
                    text.ParagraphProperties = new ParagraphProperties(new Indentation() { Left = "720" });
                    body.Append(text);
                    text = new Paragraph(new Run(new Text("- Оборудование для работы с аудио и видеоматериалами.")));
                    text.ParagraphProperties = new ParagraphProperties(new Indentation() { Left = "720" });
                    body.Append(text);
                    text = new Paragraph(new Run(new Text("4. Мебель:")));
                    body.Append(text);
                    text = new Paragraph(new Run(new Text("- Стеллажи для хранения книг;")));
                    text.ParagraphProperties = new ParagraphProperties(new Indentation() { Left = "720" });
                    body.Append(text);
                    text = new Paragraph(new Run(new Text("- Читальные столы и стулья;")));
                    text.ParagraphProperties = new ParagraphProperties(new Indentation() { Left = "720" });
                    body.Append(text);
                    text = new Paragraph(new Run(new Text("- Комфортные зоны для чтения и работы.")));
                    text.ParagraphProperties = new ParagraphProperties(new Indentation() { Left = "720" });
                    body.Append(text);
                    text = new Paragraph(new Run(new Text("5. Инвентарь:")));
                    body.Append(text);
                    text = new Paragraph(new Run(new Text("- Картотеки и каталоги для поиска литературы;")));
                    text.ParagraphProperties = new ParagraphProperties(new Indentation() { Left = "720" });
                    body.Append(text);
                    text = new Paragraph(new Run(new Text("- Оргтехника для работы библиотекарей.")));
                    text.ParagraphProperties = new ParagraphProperties(new Indentation() { Left = "720" });
                    body.Append(text);
                    text = new Paragraph(new Run(new Text("6. Программное обеспечение:")));
                    body.Append(text);
                    text = new Paragraph(new Run(new Text("- Система учета читателей и книжного фонда;")));
                    text.ParagraphProperties = new ParagraphProperties(new Indentation() { Left = "720" });
                    body.Append(text);
                    text = new Paragraph(new Run(new Text("- Базы данных для хранения информации о книгах и читателях;")));
                    text.ParagraphProperties = new ParagraphProperties(new Indentation() { Left = "720" });
                    body.Append(text);
                    text = new Paragraph(new Run(new Text("- Специализированные программы для составления отчетов.")));
                    text.ParagraphProperties = new ParagraphProperties(new Indentation() { Left = "720" });
                    body.Append(text);

                    title = new Paragraph(new Run(new Text("Количество пользователей и посещений")));
                    title.ParagraphProperties = new ParagraphProperties(new Justification() { Val = JustificationValues.Center });
                    body.Append(title);

                    ReadersInfo info = GetReadersInfo();
                    text = new Paragraph(new Run(new Text($"За {DateTime.Now.Year} год библиотеку посетило {info.TotalReaders} человек {info.Visits} раз.")));
                    text.ParagraphProperties = new ParagraphProperties(new Indentation() { Left = "720" });
                    body.Append(text);

                    title = new Paragraph(new Run(new Text("Формирование и использование библиотечного фонда")));
                    title.ParagraphProperties = new ParagraphProperties(new Justification() { Val = JustificationValues.Center });
                    body.Append(title);

                    text = new Paragraph(new Run(new Text($"В {DateTime.Now.Year} году в библиотеке брали следующие книги:")));
                    text.ParagraphProperties = new ParagraphProperties(new Indentation() { Left = "720" });
                    body.Append(text);

                    Table table = new Table();

                    int i = 1;
                    foreach (var bookTitle in books)
                    {
                        TableRow row = new TableRow();
                        row.Append(
                            new TableCell(new Paragraph(new Run(new Text(i.ToString())))),
                            new TableCell(new Paragraph(new Run(new Text(bookTitle)))
                        ));
                        table.Append(row);
                        i++;
                    }
                    body.Append(table);
                    i = 1;

                    title = new Paragraph(new Run(new Text("Персонал библиотеки")));
                    title.ParagraphProperties = new ParagraphProperties(new Justification() { Val = JustificationValues.Center });
                    body.Append(title);

                    table = new Table();
                    TableRow headerRow = new TableRow();
                    headerRow.Append(
                        new TableCell(new Paragraph(new Run(new Text("")))),
                        new TableCell(new Paragraph(new Run(new Text("Фамилия")))),
                        new TableCell(new Paragraph(new Run(new Text("Имя")))),
                        new TableCell(new Paragraph(new Run(new Text("Отчество")))),
                        new TableCell(new Paragraph(new Run(new Text("Должность")))
                    ));
                    table.Append(headerRow);
                    foreach (var person in workers)
                    {
                        TableRow row = new TableRow();
                        row.Append(
                            new TableCell(new Paragraph(new Run(new Text(i.ToString())))),
                            new TableCell(new Paragraph(new Run(new Text(person.Surname)))),
                            new TableCell(new Paragraph(new Run(new Text(person.Name)))),
                            new TableCell(new Paragraph(new Run(new Text(person.Patronymic)))),
                            new TableCell(new Paragraph(new Run(new Text(person.Title))))
                        );
                        table.Append(row);
                        i++;
                    }
                    body.Append(table);
                    mainPart.Document.Append(body);
                }

                // Сохранение в файл
                byte[] byteArray = mem.ToArray();
                return File(byteArray, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "Годовой отчет.docx");
            }
        }
    }
}