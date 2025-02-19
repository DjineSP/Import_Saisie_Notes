using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations.ImportationDb
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Etudiants",
                columns: table => new
                {
                    Matricule = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Nom = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Prenoms = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Sexe = table.Column<string>(type: "TEXT", maxLength: 1, nullable: false),
                    DateNaissance = table.Column<DateTime>(type: "TEXT", nullable: false),
                    VilleNaissance = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etudiants", x => x.Matricule);
                });

            migrationBuilder.CreateTable(
                name: "Filieres",
                columns: table => new
                {
                    IdFiliere = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CodeFiliere = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Intitule = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filieres", x => x.IdFiliere);
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    IdGrade = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CodeGrade = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.IdGrade);
                });

            migrationBuilder.CreateTable(
                name: "Niveaux",
                columns: table => new
                {
                    IdNiveau = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CodeNiveau = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Niveaux", x => x.IdNiveau);
                });

            migrationBuilder.CreateTable(
                name: "Specialites",
                columns: table => new
                {
                    IdSpecialite = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CodeSpecialite = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Intitule = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialites", x => x.IdSpecialite);
                });

            migrationBuilder.CreateTable(
                name: "UEs",
                columns: table => new
                {
                    IdUE = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CodeUE = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Intitule = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Semestre = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UEs", x => x.IdUE);
                });

            migrationBuilder.CreateTable(
                name: "AnonymatsNoms",
                columns: table => new
                {
                    NumAnonymat = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Matricule = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnonymatsNoms", x => x.NumAnonymat);
                    table.ForeignKey(
                        name: "FK_AnonymatsNoms_Etudiants_Matricule",
                        column: x => x.Matricule,
                        principalTable: "Etudiants",
                        principalColumn: "Matricule",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    IdClasse = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CodeClasse = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    IdNiveau = table.Column<int>(type: "INTEGER", nullable: false),
                    IdGrade = table.Column<int>(type: "INTEGER", nullable: false),
                    IdFiliere = table.Column<int>(type: "INTEGER", nullable: false),
                    IdSpecialite = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.IdClasse);
                    table.ForeignKey(
                        name: "FK_Classes_Filieres_IdFiliere",
                        column: x => x.IdFiliere,
                        principalTable: "Filieres",
                        principalColumn: "IdFiliere",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Classes_Grades_IdGrade",
                        column: x => x.IdGrade,
                        principalTable: "Grades",
                        principalColumn: "IdGrade",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Classes_Niveaux_IdNiveau",
                        column: x => x.IdNiveau,
                        principalTable: "Niveaux",
                        principalColumn: "IdNiveau",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Classes_Specialites_IdSpecialite",
                        column: x => x.IdSpecialite,
                        principalTable: "Specialites",
                        principalColumn: "IdSpecialite",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Evaluations",
                columns: table => new
                {
                    IdEvaluation = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdUE = table.Column<int>(type: "INTEGER", nullable: false),
                    CodeEvaluation = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Credit = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluations", x => x.IdEvaluation);
                    table.ForeignKey(
                        name: "FK_Evaluations_UEs_IdUE",
                        column: x => x.IdUE,
                        principalTable: "UEs",
                        principalColumn: "IdUE",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Inscrits",
                columns: table => new
                {
                    IdInscrit = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Matricule = table.Column<string>(type: "TEXT", nullable: false),
                    IdClasse = table.Column<int>(type: "INTEGER", nullable: false),
                    Annee = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscrits", x => x.IdInscrit);
                    table.ForeignKey(
                        name: "FK_Inscrits_Classes_IdClasse",
                        column: x => x.IdClasse,
                        principalTable: "Classes",
                        principalColumn: "IdClasse",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inscrits_Etudiants_Matricule",
                        column: x => x.Matricule,
                        principalTable: "Etudiants",
                        principalColumn: "Matricule",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Programmes",
                columns: table => new
                {
                    IdProgramme = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdClasse = table.Column<int>(type: "INTEGER", nullable: false),
                    IdUE = table.Column<int>(type: "INTEGER", nullable: false),
                    Professeur = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programmes", x => x.IdProgramme);
                    table.ForeignKey(
                        name: "FK_Programmes_Classes_IdClasse",
                        column: x => x.IdClasse,
                        principalTable: "Classes",
                        principalColumn: "IdClasse",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Programmes_UEs_IdUE",
                        column: x => x.IdUE,
                        principalTable: "UEs",
                        principalColumn: "IdUE",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnonymatsNotes",
                columns: table => new
                {
                    IdAnonymatNote = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NumAnonymat = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    IdEvaluation = table.Column<int>(type: "INTEGER", nullable: false),
                    Note = table.Column<decimal>(type: "TEXT", nullable: false),
                    Observation = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnonymatsNotes", x => x.IdAnonymatNote);
                    table.ForeignKey(
                        name: "FK_AnonymatsNotes_AnonymatsNoms_NumAnonymat",
                        column: x => x.NumAnonymat,
                        principalTable: "AnonymatsNoms",
                        principalColumn: "NumAnonymat",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnonymatsNotes_Evaluations_IdEvaluation",
                        column: x => x.IdEvaluation,
                        principalTable: "Evaluations",
                        principalColumn: "IdEvaluation",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotesCC",
                columns: table => new
                {
                    IdNoteCC = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdEvaluation = table.Column<int>(type: "INTEGER", nullable: false),
                    Matricule = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Note = table.Column<decimal>(type: "TEXT", nullable: false),
                    Observation = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotesCC", x => x.IdNoteCC);
                    table.ForeignKey(
                        name: "FK_NotesCC_Etudiants_Matricule",
                        column: x => x.Matricule,
                        principalTable: "Etudiants",
                        principalColumn: "Matricule",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotesCC_Evaluations_IdEvaluation",
                        column: x => x.IdEvaluation,
                        principalTable: "Evaluations",
                        principalColumn: "IdEvaluation",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NotesTP",
                columns: table => new
                {
                    IdNoteTP = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdEvaluation = table.Column<int>(type: "INTEGER", nullable: false),
                    Matricule = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Note = table.Column<decimal>(type: "TEXT", nullable: false),
                    Observation = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotesTP", x => x.IdNoteTP);
                    table.ForeignKey(
                        name: "FK_NotesTP_Etudiants_Matricule",
                        column: x => x.Matricule,
                        principalTable: "Etudiants",
                        principalColumn: "Matricule",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotesTP_Evaluations_IdEvaluation",
                        column: x => x.IdEvaluation,
                        principalTable: "Evaluations",
                        principalColumn: "IdEvaluation",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Etudiants",
                columns: new[] { "Matricule", "DateNaissance", "Nom", "Prenoms", "Sexe", "VilleNaissance" },
                values: new object[,]
                {
                    { "23U2292", new DateTime(2005, 10, 19, 12, 0, 0, 0, DateTimeKind.Unspecified), "Djine", "Sinto Pafing", "M", "Moundou" },
                    { "23U2293", new DateTime(2005, 10, 19, 12, 0, 0, 0, DateTimeKind.Unspecified), "Alban", "Kouyabe Pafing", "M", "Moundou" }
                });

            migrationBuilder.InsertData(
                table: "Filieres",
                columns: new[] { "IdFiliere", "CodeFiliere", "Intitule", "Title" },
                values: new object[,]
                {
                    { 1, "INFO", "Informatique fondamental", "Fondamental Computer Science" },
                    { 2, "ICT4D", "Technologie de l'Information et de la Communication pour le Developpement", "Information and Communication Technology For Development" },
                    { 3, "MATHS", "Mathematiques", "Mathematics" }
                });

            migrationBuilder.InsertData(
                table: "Grades",
                columns: new[] { "IdGrade", "CodeGrade" },
                values: new object[,]
                {
                    { 1, "Licence" },
                    { 2, "Master" },
                    { 3, "Doctorat" }
                });

            migrationBuilder.InsertData(
                table: "Niveaux",
                columns: new[] { "IdNiveau", "CodeNiveau" },
                values: new object[,]
                {
                    { 1, "L1" },
                    { 2, "L2" },
                    { 3, "L3" },
                    { 4, "M1" },
                    { 5, "M2" },
                    { 6, "D" }
                });

            migrationBuilder.InsertData(
                table: "Specialites",
                columns: new[] { "IdSpecialite", "CodeSpecialite", "Intitule", "Title" },
                values: new object[,]
                {
                    { 1, "None", "", "" },
                    { 2, "GL", "Genie Logiciel", "Sotfware Engeneering" },
                    { 3, "Secu", "Cyber Securite", "Cyber Security" },
                    { 4, "Reseau", "Reseau Informatique", "Computer Networking" }
                });

            migrationBuilder.InsertData(
                table: "UEs",
                columns: new[] { "IdUE", "CodeUE", "Intitule", "Semestre", "Title" },
                values: new object[,]
                {
                    { 1, "ICT201", "", "S1", "" },
                    { 2, "ICT203", "", "S1", "" },
                    { 3, "ICT205", "", "S1", "" },
                    { 4, "ICT207", "", "S1", "" },
                    { 5, "ICT202", "", "S2", "" }
                });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "IdClasse", "CodeClasse", "IdFiliere", "IdGrade", "IdNiveau", "IdSpecialite" },
                values: new object[,]
                {
                    { 1, "ICT L1", 2, 1, 1, 1 },
                    { 2, "ICT L2", 2, 1, 2, 1 },
                    { 3, "ICT L2-GL", 2, 1, 2, 2 },
                    { 4, "ICT L2-Secu", 2, 1, 2, 3 },
                    { 5, "ICT L2-Reseau", 2, 1, 2, 4 },
                    { 6, "ICT L3", 2, 1, 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "Inscrits",
                columns: new[] { "IdInscrit", "Annee", "IdClasse", "Matricule" },
                values: new object[,]
                {
                    { 1, "2024/2025", 2, "23U2292" },
                    { 2, "2024/2025", 1, "23U2293" }
                });

            migrationBuilder.InsertData(
                table: "Programmes",
                columns: new[] { "IdProgramme", "IdClasse", "IdUE", "Professeur" },
                values: new object[,]
                {
                    { 1, 2, 1, "Djine" },
                    { 2, 2, 2, "Djine" },
                    { 3, 2, 3, "Djine" },
                    { 4, 2, 4, "Djine" },
                    { 5, 2, 5, "Djine" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnonymatsNoms_Matricule",
                table: "AnonymatsNoms",
                column: "Matricule");

            migrationBuilder.CreateIndex(
                name: "IX_AnonymatsNotes_IdEvaluation",
                table: "AnonymatsNotes",
                column: "IdEvaluation");

            migrationBuilder.CreateIndex(
                name: "IX_AnonymatsNotes_NumAnonymat",
                table: "AnonymatsNotes",
                column: "NumAnonymat");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_IdFiliere",
                table: "Classes",
                column: "IdFiliere");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_IdGrade",
                table: "Classes",
                column: "IdGrade");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_IdNiveau",
                table: "Classes",
                column: "IdNiveau");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_IdSpecialite",
                table: "Classes",
                column: "IdSpecialite");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_IdUE",
                table: "Evaluations",
                column: "IdUE");

            migrationBuilder.CreateIndex(
                name: "IX_Inscrits_IdClasse",
                table: "Inscrits",
                column: "IdClasse");

            migrationBuilder.CreateIndex(
                name: "IX_Inscrits_Matricule",
                table: "Inscrits",
                column: "Matricule");

            migrationBuilder.CreateIndex(
                name: "IX_NotesCC_IdEvaluation",
                table: "NotesCC",
                column: "IdEvaluation");

            migrationBuilder.CreateIndex(
                name: "IX_NotesCC_Matricule",
                table: "NotesCC",
                column: "Matricule");

            migrationBuilder.CreateIndex(
                name: "IX_NotesTP_IdEvaluation",
                table: "NotesTP",
                column: "IdEvaluation");

            migrationBuilder.CreateIndex(
                name: "IX_NotesTP_Matricule",
                table: "NotesTP",
                column: "Matricule");

            migrationBuilder.CreateIndex(
                name: "IX_Programmes_IdClasse",
                table: "Programmes",
                column: "IdClasse");

            migrationBuilder.CreateIndex(
                name: "IX_Programmes_IdUE",
                table: "Programmes",
                column: "IdUE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnonymatsNotes");

            migrationBuilder.DropTable(
                name: "Inscrits");

            migrationBuilder.DropTable(
                name: "NotesCC");

            migrationBuilder.DropTable(
                name: "NotesTP");

            migrationBuilder.DropTable(
                name: "Programmes");

            migrationBuilder.DropTable(
                name: "AnonymatsNoms");

            migrationBuilder.DropTable(
                name: "Evaluations");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Etudiants");

            migrationBuilder.DropTable(
                name: "UEs");

            migrationBuilder.DropTable(
                name: "Filieres");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "Niveaux");

            migrationBuilder.DropTable(
                name: "Specialites");
        }
    }
}
