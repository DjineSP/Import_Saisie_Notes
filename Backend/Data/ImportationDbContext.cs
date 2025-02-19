using Backend.Models.BonitaModels;
using Backend.Models.ImportationModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace Backend.Data;

public class ImportationDbContext : DbContext
{
    public ImportationDbContext(DbContextOptions<ImportationDbContext> options) : base(options) { }

    // DbSets pour chaque entité
    public DbSet<Etudiant>? Etudiants { get; set; }
    public DbSet<Niveau>? Niveaux { get; set; }
    public DbSet<Grade>? Grades { get; set; }
    public DbSet<Filiere>? Filieres { get; set; }
    public DbSet<Specialite>? Specialites { get; set; }
    public DbSet<Classe>? Classes { get; set; }
    public DbSet<UE>? UEs { get; set; }
    public DbSet<Inscrit>? Inscrits { get; set; }
    public DbSet<Evaluation>? Evaluations { get; set; }
    public DbSet<AnonymatNom>? AnonymatsNoms { get; set; }
    public DbSet<AnonymatNote>? AnonymatsNotes { get; set; }
    public DbSet<NoteCC>? NotesCC { get; set; }
    public DbSet<NoteTP>? NotesTP { get; set; }
    public DbSet<Programme>? Programmes { get; set; }

    // Configuration des relations
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //Relation entre Etudiant - Inscrit (1, N)
        modelBuilder.Entity<Inscrit>()
            .HasOne(i => i.Etudiant)
            .WithMany()
            .HasForeignKey(i => i.Matricule)
            .OnDelete(DeleteBehavior.Restrict);

        //Relation entre Etudiant - AnonymatNom(1, N)
        modelBuilder.Entity<AnonymatNom>()
            .HasOne(a => a.Etudiant)
            .WithMany()
            .HasForeignKey(a => a.Matricule)
            .OnDelete(DeleteBehavior.Restrict);

        // Relation entre Etudiant - NoteCC (1,N)
        modelBuilder.Entity<NoteCC>()
            .HasOne(n => n.Etudiant)
            .WithMany()
            .HasForeignKey(n => n.Matricule)
            .OnDelete(DeleteBehavior.Cascade);

        // Relation entre Etudiant - NoteTP (1,N)
        modelBuilder.Entity<NoteTP>()
            .HasOne(n => n.Etudiant)
            .WithMany()
            .HasForeignKey(n => n.Matricule)
            .OnDelete(DeleteBehavior.Cascade);

        // Relation entre Niveau - Classe(1, N)
        modelBuilder.Entity<Classe>()
            .HasOne(c => c.Niveau)
            .WithMany()
            .HasForeignKey(c => c.IdNiveau)
            .OnDelete(DeleteBehavior.Restrict);

        // Relation entre Grade - Classe(1, N)
        modelBuilder.Entity<Classe>()
           .HasOne(c => c.Grade)
           .WithMany()
           .HasForeignKey(c => c.IdGrade)
           .OnDelete(DeleteBehavior.Restrict);

        // Relation entre Specialite - Classe(1, N)
        modelBuilder.Entity<Classe>()
            .HasOne(s => s.Specialite)
            .WithMany()
            .HasForeignKey(s => s.IdSpecialite)
            .OnDelete(DeleteBehavior.Restrict);

        // Relation entre Filiere - Classe(1, N)
        modelBuilder.Entity<Classe>()
            .HasOne(s => s.Filiere)
            .WithMany()
            .HasForeignKey(s => s.IdFiliere)
            .OnDelete(DeleteBehavior.Restrict);

        // Relation entre Classe - Inscrit(1, N)
        modelBuilder.Entity<Inscrit>()
            .HasOne(i => i.Classe)
            .WithMany()
            .HasForeignKey(i => i.IdClasse)
            .OnDelete(DeleteBehavior.Restrict);

        //Relation entre UE - Evaluation(1, N)
        modelBuilder.Entity<Evaluation>()
            .HasOne(i => i.UE)
            .WithMany()
            .HasForeignKey(i => i.IdUE)
            .OnDelete(DeleteBehavior.Restrict);

        // Relation entre Evaluation - NoteCC(1, N)
        modelBuilder.Entity<NoteCC>()
            .HasOne(i => i.Evaluation)
            .WithMany()
            .HasForeignKey(i => i.IdEvaluation)
            .OnDelete(DeleteBehavior.Restrict);

        // Relation entre Evaluation - NoteTP(1, N)
        modelBuilder.Entity<NoteTP>()
            .HasOne(i => i.Evaluation)
            .WithMany()
            .HasForeignKey(i => i.IdEvaluation)
            .OnDelete(DeleteBehavior.Restrict);

        // Relation entre Evaluation - AnonymatNote (1,N)
        modelBuilder.Entity<AnonymatNote>()
            .HasOne(an => an.Evaluation)
            .WithMany()
            .HasForeignKey(an => an.IdEvaluation)
            .OnDelete(DeleteBehavior.Cascade);


        // Relation entre AnonymatNom - AnonymatNote (1,N)
        modelBuilder.Entity<AnonymatNote>()
            .HasOne(an => an.AnonymatNom)
            .WithMany()
            .HasForeignKey(an => an.NumAnonymat)
            .OnDelete(DeleteBehavior.Restrict);

        // Relation entre Classe - Programme (1,N)
        modelBuilder.Entity<Programme>()
            .HasOne(an => an.Classe)
            .WithMany()
            .HasForeignKey(an => an.IdClasse)
            .OnDelete(DeleteBehavior.Restrict);

        // Relation entre UE - Programme (1,N)
        modelBuilder.Entity<Programme>()
            .HasOne(an => an.UE)
            .WithMany()
            .HasForeignKey(an => an.IdUE)
            .OnDelete(DeleteBehavior.Restrict);

        // Ajout de données de tests
        

        modelBuilder.Entity<Niveau>().HasData(
            new Niveau
            {
                IdNiveau = 1,
                CodeNiveau = "L1"
            },
            new Niveau
            {
                IdNiveau = 2,
                CodeNiveau = "L2"
            },
            new Niveau
            {
                IdNiveau = 3,
                CodeNiveau = "L3"
            },
            new Niveau
            {
                IdNiveau = 4,
                CodeNiveau = "M1"
            },
            new Niveau
            {
                IdNiveau = 5,
                CodeNiveau = "M2"
            },
            new Niveau
            {
                IdNiveau = 6,
                CodeNiveau = "D"
            }
        );


        modelBuilder.Entity<Grade>().HasData(
           new Grade
           {
               IdGrade = 1,
               CodeGrade = "Licence"
           },
            new Grade
            {
                IdGrade = 2,
                CodeGrade = "Master"
            },
             new Grade
             {
                 IdGrade = 3,
                 CodeGrade = "Doctorat"
             }
       );

        modelBuilder.Entity<Specialite>().HasData(
          new Specialite
          {
              IdSpecialite = 1,
              CodeSpecialite = "None",
              Intitule = "",
              Title = ""
          },
          new Specialite
          {
              IdSpecialite = 2,
              CodeSpecialite = "GL",
              Intitule = "Genie Logiciel",
              Title = "Sotfware Engeneering"
          },
          new Specialite
          {
              IdSpecialite = 3,
              CodeSpecialite = "Secu",
              Intitule = "Cyber Securite",
              Title = "Cyber Security"
          },
          new Specialite
          {
              IdSpecialite = 4,
              CodeSpecialite = "Reseau",
              Intitule = "Reseau Informatique",
              Title = "Computer Networking"

          }
        );

        modelBuilder.Entity<Filiere>().HasData(
           new Filiere
           {
                IdFiliere = 1,
                CodeFiliere = "INFO",
                Intitule = "Informatique fondamental",
                Title = "Fondamental Computer Science"
           },

            new Filiere
            {
                IdFiliere = 2,
                CodeFiliere = "ICT4D",
                Intitule = "Technologie de l'Information et de la Communication pour le Developpement",
                Title = "Information and Communication Technology For Development"
            },
            new Filiere
            {
                IdFiliere = 3,
                CodeFiliere = "MATHS",
                Intitule = "Mathematiques",
                Title = "Mathematics"
            }
       );


        modelBuilder.Entity<Classe>().HasData(
         new Classe
         {
             IdClasse = 1,
             CodeClasse = "ICT L1",
             IdNiveau = 1,
             IdFiliere = 2,
             IdGrade = 1,
             IdSpecialite = 1
         },
         new Classe
         {
             IdClasse = 2,
             CodeClasse = "ICT L2",
             IdNiveau = 2,
             IdFiliere = 2,
             IdGrade = 1,
             IdSpecialite = 1
         },
         new Classe
         {
             IdClasse = 3,
             CodeClasse = "ICT L2-GL",
             IdNiveau = 2,
             IdFiliere = 2,
             IdGrade = 1,
             IdSpecialite = 2
         },
         new Classe
         {
             IdClasse = 4,
             CodeClasse = "ICT L2-Secu",
             IdNiveau = 2,
             IdFiliere = 2,
             IdGrade = 1,
             IdSpecialite = 3
         },
         new Classe
         {
             IdClasse = 5,
             CodeClasse = "ICT L2-Reseau",
             IdNiveau = 2,
             IdGrade = 1,
             IdFiliere = 2,
             IdSpecialite = 4
         },
         new Classe
         {
             IdClasse = 6,
             CodeClasse = "ICT L3",
             IdNiveau = 2,
             IdGrade = 1,
             IdFiliere = 2,
             IdSpecialite = 1
         }
     );

        modelBuilder.Entity<UE>().HasData(
         new UE
         {
             IdUE = 1,
             CodeUE = "ICT201",
             Intitule = "",
             Title = "",
             Semestre = "S1"
         },
          new UE
          {
              IdUE = 2,
              CodeUE = "ICT203",
              Intitule = "",
              Title = "",
              Semestre = "S1"
          },
           new UE
           {
               IdUE = 3,
               CodeUE = "ICT205",
               Intitule = "",
               Title = "",
               Semestre = "S1"
           },
           new UE
           {
               IdUE = 4,
               CodeUE = "ICT207",
               Intitule = "",
               Title = "",
               Semestre = "S1"
           },
           new UE
           {
               IdUE = 5,
               CodeUE = "ICT202",
               Intitule = "",
               Title = "",
               Semestre = "S2"
           }

        );

       

        modelBuilder.Entity<Programme>().HasData(
          new Programme
          {
               IdProgramme = 1,
               IdClasse = 2, // ICT L2
               IdUE = 1,// Première UE
               Professeur = "Djine"
          },
          new Programme
          {
               IdProgramme = 2,
               IdClasse = 2, // ICT L2
               IdUE = 2, // Deuxième UE
               Professeur = "Djine"
          },
          new Programme
          {
               IdProgramme = 3,
               IdClasse = 2, // ICT L2
               IdUE = 3, // Troisième UE
               Professeur = "Djine"
          },
           new Programme
           {
               IdProgramme = 4,
               IdClasse = 2, // ICT L2
               IdUE = 4, // Remplacez par l'ID de la dernière UE
               Professeur = "Djine"
           },
           new Programme
           {
               IdProgramme = 5,
               IdClasse = 2, // ICT L2
               IdUE = 5, // Remplacez par l'ID de la dernière UE
               Professeur = "Djine"
           }
        );

        modelBuilder.Entity<Etudiant>().HasData(
    new Etudiant
    {
        Matricule = "23U2292",
        Nom = "Djine",
        Prenoms = "Sinto Pafing",
        Sexe = "M",
        DateNaissance = new DateTime(2005, 10, 19, 12, 0, 0),
        VilleNaissance = "Moundou"
    },
    new Etudiant
    {
        Matricule = "23U2293",
        Nom = "Alban",
        Prenoms = "Kouyabe Pafing",
        Sexe = "M",
        DateNaissance = new DateTime(2005, 10, 19, 12, 0, 0),
        VilleNaissance = "Moundou"
    }
    );
        modelBuilder.Entity<Inscrit>().HasData(
             new Inscrit
             {
                 IdInscrit = 1,
                 Matricule = "23U2292",
                 IdClasse = 2,
                 Annee = "2024/2025"
             },
              new Inscrit
              {
                  IdInscrit = 2,
                  Matricule = "23U2293",
                  IdClasse = 1,
                  Annee = "2024/2025"
              }
             );

    }
}
