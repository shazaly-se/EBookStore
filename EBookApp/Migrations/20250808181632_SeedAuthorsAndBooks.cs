using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EBookApp.Migrations
{
    public partial class SeedAuthorsAndBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfilePicture = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoverImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Bio", "Name", "ProfilePicture" },
                values: new object[] { 1, "Alexandre Malavasi has over 17 years of experience in software engineering, working on large-scale projects in Brazil, Europe, and the United States. He is currently the co-founder and CTO of MARELO, an innovative company focused on the international logistics market. Throughout his career, he has led and contributed to groundbreaking projects with a global impact, taking on various roles in software engineering and technology management, and leading large multinational teams across Europe, the United States, and other regions.\r\n\r\nAdditionally, he has been awarded the Microsoft MVP title four times for his contributions to technical and professional communities in several countries across Europe and South America. He is also the author of four international books on .NET and has had the opportunity to speak at major conferences worldwide, including in Poland, Ukraine, Nepal, England, and other countries. During these events, he has shared insights on software architecture, Azure, .NET, and other technologies.\r\n\r\nHis most recent academic background includes two postgraduate degrees: one in Software Engineering with an emphasis on Agile Methods and another in Systems Design and Development. He also holds multiple Microsoft certifications and has completed dozens of additional training programs. Alexandre is passionate about sharing knowledge and contributing to the tech community.\r\n\r\nIn his free time, he organizes tech events, provides free mentorship to professionals and students worldwide, gives lectures, and writes articles on software development and technological innovation.", "Alexandre F Malavasi Cardoso", "Alexandre.jpg" });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Bio", "Name", "ProfilePicture" },
                values: new object[] { 2, "Mark J Price is a former Microsoft Certified Trainer (MCT) and current Microsoft Specialist: Programming in C# and Architecting Microsoft Azure Solutions, with more than 20 years' of educational and programming experience. \r\n\r\nSince 1993 Mark has passed more than 80 Microsoft programming exams and specializes in preparing others to pass them too. His students range from professionals with decades of experience to 16-year-old apprentices with none. Mark successfully guides all of them by combining educational skills with real-world experience consulting and developing systems for enterprises worldwide. \r\n\r\nBetween 2001 and 2003 Mark was employed full-time to write official courseware for Microsoft in Redmond, USA. Mark's team wrote the first training courses for C# while it was still an early alpha version. While with Microsoft he taught \"train-the-trainer\" classes to get other MCTs up-to-speed on C# and .NET. \r\n\r\nCurrently, Mark creates and delivers training courses for Episerver's Digital Experience Platform, the best .NET CMS for Digital Marketing and E-commerce. \r\n\r\nIn 2010 Mark studied for a Post-Graduate Certificate in Education (PGCE). He taught GCSE and A-Level mathematics in two London secondary schools. Mark holds a Computer Science BSc. Hons. Degree from the University of Bristol, UK.", "Mark J. Price", "Mark.jpg" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "CoverImage", "Description", "Price", "Title" },
                values: new object[] { 1, 1, "Modern-Full-Stack-Web-Development.jpg", "In the ASP.NET Core ecosystem, choosing the right JavaScript framework is key to addressing diverse project requirements. Witten by a four-time Microsoft MVP with 16 years of software development experience, this book is your comprehensive guide to mastering full-stack development, combining ASP.NET Core’s robust backend capabilities with the dynamic frontend power of Vue.js, Angular, and React.\r\n\r\nThis book uses ASP.NET Core to teach you best practices for integrating modern JavaScript frameworks, and creating responsive, high-performance applications that deliver seamless client–server interactions and scalable RESTful APIs. In addition to building expertise in ASP.NET Core’s core strengths, such as API security, architecture principles, and performance optimization, the chapters will help you develop the essential frontend skills needed for full-stack development. Sections on Blazor take a C#-based approach to creating interactive UIs, showcasing ASP.NET Core’s flexibility in handling both server and client-side needs.\r\n\r\nBy the end of this book, you will have a complete toolkit for designing, deploying, and maintaining complex full-stack applications, along with practical knowledge of both backend and frontend development.", 162m, "Modern Full-Stack Web Development with ASP.NET Core: A project-based guide to building web applications with ASP.NET Core 9 and JavaScript frameworks" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "CoverImage", "Description", "Price", "Title" },
                values: new object[] { 2, 2, "Apps-and-Services-with-NET8.jpg", "Elevate your practical C# and .NET skills to the next level with this new edition of Apps and Services with .NET 8.\r\n\r\nWith chapters that put a variety of technologies into practice, including Web API, gRPC, GraphQL, and SignalR, this book will give you a broader scope of knowledge than other books that often focus on only a handful of .NET technologies. You'll dive into the new unified model for Blazor Full Stack and leverage .NET MAUI to develop mobile and desktop apps.\r\n\r\nThis new edition introduces the latest enhancements, including the seamless implementation of web services with ADO.NET SqlClient's native Ahead-of-Time (AOT) support. Popular library coverage now includes Humanizer and Noda Time. There's also a brand-new chapter that delves into service architecture, caching, queuing, and robust background services.\r\n\r\nBy the end of this book, you'll have a wide range of best practices and deep insights under your belt to help you build rich apps and efficient services.", 224m, "Packt Apps and Services with .NET 8 - Second Edition: Build practical projects with Blazor, .NET MAUI, gRPC, GraphQL, and other enterprise technologies" });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
