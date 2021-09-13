using System;
using System.Linq;
using DesafioAPI.Models;

namespace DesafioAPI.Data
{
    public class SeedService
    {
        private readonly ApplicationDbContext database;

        public SeedService(ApplicationDbContext db)
        {
            this.database = db;
        }

        public void Seed()
        {
            if (database.Adresses.Any() ||
                database.Arrests.Any() ||
                database.Autopsies.Any() ||
                database.Coroners.Any() ||
                database.Crimes.Any() ||
                database.Deputies.Any() ||
                database.Perpetrators.Any() ||
                database.PoliceDepartments.Any() ||
                database.PoliceOfficers.Any() ||
                database.Users.Any() ||
                database.Victims.Any()
            ) return;
            //Users
            User[] users = new User[2];
            users[0] = new User() { Name = "Lulay", CPF = "234.567.800-99", RegisterId = "lulay", UserRole = "Judge", Status = true, Password = "$2b$10$XRyEC0dLHCZizL5sudXqcucXBXEMxUHmmwcXDLrqqQm8Csv.C61Dq" }; //Password = gft2021
            users[1] = new User() { Name = "Advogado Dredd", CPF = "098.756.543-21", RegisterId = "i am the law", UserRole = "Lawyer", Status = true, Password = "$2b$10$xE28cZ98TXk47pG46HwBtumfJ.hFSFg6cc0fgY990oYY0.uRLawLW" }; //Password = stalone
            database.Users.AddRange(users); //adding Lulay(uid="lulay"; pw="gft2021") and Advogado Dredd(uid="i am the law"; pw="stalone") to DB
            database.SaveChanges();

            //Perpetrators (Criminosos)
            Perpetrator[] perpetrators = new Perpetrator[5];
            perpetrators[0] = new Perpetrator() { Name = "Jair Messias Bolsonaro", CPF = "666.666.666-66", Status = true };
            perpetrators[1] = new Perpetrator() { Name = "Chico Picadinho", CPF = "111.111.111-11", Status = true };
            perpetrators[2] = new Perpetrator() { Name = "Joao de Deus", CPF = "222.222.222-22", Status = true };
            perpetrators[3] = new Perpetrator() { Name = "Pedrinho Matador", CPF = "333.333.333-33", Status = true };
            perpetrators[4] = new Perpetrator() { Name = "Lazaro", CPF = "444.444.444-44", Status = true };
            database.Perpetrators.AddRange(perpetrators);
            database.SaveChanges();

            //Victims
            Victim[] victims = new Victim[5];
            victims[0] = new Victim() { Name = "Cher", CPF = "000.000.000-00", Status = true };
            victims[1] = new Victim() { Name = "Joao Bosco", CPF = "123.123.123-12", Status = true };
            victims[2] = new Victim() { Name = "Tom do Jerry", CPF = "234.234.234-23", Status = true };
            victims[3] = new Victim() { Name = "Sebastiana Bastiana", CPF = "345.345.345-34", Status = true };
            victims[4] = new Victim() { Name = "Uzumaki Naruto", CPF = "456.456.456-45", Status = true };
            database.Victims.AddRange(victims);
            database.SaveChanges();

            //Addresses
            Adress[] adresses = new Adress[5];
            adresses[0] = new Adress() { Street = "Rua 5-E", Number = "10", City = "Maceió", State = "AL", ZIPCode = "57084-632", Status = true };
            adresses[1] = new Adress() { Street = "Rua Adelina Bastos", Number = "11", City = "Ilhéus", State = "BA", ZIPCode = "45654-030", Status = true };
            adresses[2] = new Adress() { Street = "Rua Adelina Bastos", Number = "12", City = "Ilhéus", State = "BA", ZIPCode = "45654-030", Status = true };
            adresses[3] = new Adress() { Street = "Quadra QNP 23", Number = "13", City = "Brasília", State = "DF", ZIPCode = "72242-050", Status = true };
            adresses[4] = new Adress() { Street = "Rua Otoniel Menezes", Number = "14", City = "Natal", State = "RN", ZIPCode = "59010-510", Status = true };
            database.Adresses.AddRange(adresses);
            database.SaveChanges();

            //PoliceDepartments = "PDs" (Delegacias)
            PoliceDepartment[] PDs = new PoliceDepartment[5];
            PDs[0] = new PoliceDepartment() { Adress = adresses[0], PhoneNumber = "1111-1111", Name = "Departamento Bacana", Status = true };
            PDs[1] = new PoliceDepartment() { Adress = adresses[1], PhoneNumber = "2222-2222", Name = "Departamento Supimpa", Status = true };
            PDs[2] = new PoliceDepartment() { Adress = adresses[2], PhoneNumber = "3333-3333", Name = "Departamento do Geremias", Status = true };
            PDs[3] = new PoliceDepartment() { Adress = adresses[3], PhoneNumber = "4444-4444", Name = "Departamento Gonzaguinha", Status = true };
            PDs[4] = new PoliceDepartment() { Adress = adresses[4], PhoneNumber = "5555-5555", Name = "Departamento Linkin Park", Status = true };
            database.PoliceDepartments.AddRange(PDs);
            database.SaveChanges();

            //Deputies (Delegados)
            Deputy[] deputies = new Deputy[5];
            deputies[0] = new Deputy() { Name = "Jorge Papareli", CPF = "567.567.567-56", PoliceDepartment = PDs[0], Shift = "Morning", RegisterId = "123jorge", Status = true };
            deputies[1] = new Deputy() { Name = "Joao da Silva", CPF = "678.687.678-67", PoliceDepartment = PDs[1], Shift = "Evening", RegisterId = "123joao", Status = true };
            deputies[2] = new Deputy() { Name = "Anderson Silva", CPF = "789.789.789-78", PoliceDepartment = PDs[2], Shift = "Night", RegisterId = "123anderson", Status = true };
            deputies[3] = new Deputy() { Name = "Zebedeu Carmillo", CPF = "890.890.890-89", PoliceDepartment = PDs[3], Shift = "Morning", RegisterId = "123zebedeu", Status = true };
            deputies[4] = new Deputy() { Name = "Vlad Tepes II", CPF = "123.321.123-32", PoliceDepartment = PDs[4], Shift = "Evening", RegisterId = "123vlad", Status = true };
            database.Deputies.AddRange(deputies);
            database.SaveChanges();

            //PoliceOfficers = "POs" (Policiais)
            PoliceOfficer[] POs = new PoliceOfficer[5];
            POs[0] = new PoliceOfficer() { Name = "Capitao Tanaka", CPF = "234.432.234-43", RegisterId = "123tanaka", Rank = "Police captain", Status = true };
            POs[1] = new PoliceOfficer() { Name = "Guarda Belo", CPF = "345.543.345-54", RegisterId = "123Belo", Rank = "Police officer/patrol officer/police detective", Status = true };
            POs[2] = new PoliceOfficer() { Name = "John McClane", CPF = "456.654.456-65", RegisterId = "123john", Rank = "Police corporal", Status = true };
            POs[3] = new PoliceOfficer() { Name = "Harvey Dent", CPF = "567.765.567-76", RegisterId = "123harvey", Rank = "Chief of police", Status = true };
            POs[4] = new PoliceOfficer() { Name = "John Rambo", CPF = "678.876.678-87", RegisterId = "123rambo", Rank = "Police lieutenant", Status = true };
            database.PoliceOfficers.AddRange(POs);
            database.SaveChanges();

            //Coroners (Legistas)
            Coroner[] coroners = new Coroner[5];
            coroners[0] = new Coroner() { Name = "Amanda Baiao", CPF = "908.098.098-09", RegisterId = "123baiao", Status = true };
            coroners[1] = new Coroner() { Name = "Joao Alecrim", CPF = "987.987.987-98", RegisterId = "123alecrim", Status = true };
            coroners[2] = new Coroner() { Name = "Orochimaru Azul", CPF = "876.876.876-87", RegisterId = "123orochimaru", Status = true };
            coroners[3] = new Coroner() { Name = "Vegeta da Silva", CPF = "765.765.765-76", RegisterId = "123vegeta", Status = true };
            coroners[4] = new Coroner() { Name = "Bruce Mandioca", CPF = "654.654.654-65", RegisterId = "123mandioca", Status = true };
            database.Coroners.AddRange(coroners);
            database.SaveChanges();

            //Crimes (Crimes)
            Crime[] crimes = new Crime[5];
            crimes[0] = new Crime() { Perpetrator = perpetrators[0], Victim = victims[0], Date = new DateTime(2016, 01, 10), Description = "The Perpetrator tried to blow up the victim´s house", Adress = adresses[0], Status = true };
            crimes[1] = new Crime() { Perpetrator = perpetrators[1], Victim = victims[1], Date = new DateTime(2017, 02, 11), Description = "The perpetrator tried to steal victim´s phone", Adress = adresses[1], Status = true };
            crimes[2] = new Crime() { Perpetrator = perpetrators[2], Victim = victims[2], Date = new DateTime(2018, 03, 12), Description = "The perpetrator tried to injure the victim", Adress = adresses[2], Status = true };
            crimes[3] = new Crime() { Perpetrator = perpetrators[3], Victim = victims[3], Date = new DateTime(2019, 04, 13), Description = "The perpetrator tried to steal the victim´s dog", Adress = adresses[3], Status = true };
            crimes[4] = new Crime() { Perpetrator = perpetrators[4], Victim = victims[4], Date = new DateTime(2020, 04, 14), Description = "The perpetrator stole victim´s bag", Adress = adresses[4], Status = true };
            database.Crimes.AddRange(crimes);
            database.SaveChanges();

            //Autopsies (Autopsias)
            Autopsy[] autopsies = new Autopsy[5];
            autopsies[0] = new Autopsy() { Victim = victims[0], Coroner = coroners[0], Date = new DateTime(2021, 07, 25), Description = "The autopsy went fine", Status = true };
            autopsies[1] = new Autopsy() { Victim = victims[1], Coroner = coroners[1], Date = new DateTime(2021, 07, 26), Description = "The autopsy went ok", Status = true };
            autopsies[2] = new Autopsy() { Victim = victims[2], Coroner = coroners[2], Date = new DateTime(2021, 07, 27), Description = "The autopsy went meh", Status = true };
            autopsies[3] = new Autopsy() { Victim = victims[3], Coroner = coroners[3], Date = new DateTime(2021, 07, 28), Description = "The autopsy went wrong", Status = true };
            autopsies[4] = new Autopsy() { Victim = victims[4], Coroner = coroners[4], Date = new DateTime(2021, 07, 29), Description = "The autopsy went very wrong, THE CORPSE IS ALIVE!", Status = true };
            database.Autopsies.AddRange(autopsies);
            database.SaveChanges();

            //Arrest (Prisões)
            Arrest[] arrests = new Arrest[5];
            arrests[0] = new Arrest() { Officer = POs[0], Deputy = deputies[0], Crime = crimes[0], Perpetrator = perpetrators[0], Date = new DateTime(2021, 04, 15), Status = true };
            arrests[1] = new Arrest() { Officer = POs[1], Deputy = deputies[1], Crime = crimes[1], Perpetrator = perpetrators[1], Date = new DateTime(2021, 04, 15), Status = true };
            arrests[2] = new Arrest() { Officer = POs[2], Deputy = deputies[2], Crime = crimes[2], Perpetrator = perpetrators[2], Date = new DateTime(2021, 04, 15), Status = true };
            arrests[3] = new Arrest() { Officer = POs[3], Deputy = deputies[3], Crime = crimes[3], Perpetrator = perpetrators[3], Date = new DateTime(2021, 04, 15), Status = true };
            arrests[4] = new Arrest() { Officer = POs[4], Deputy = deputies[4], Crime = crimes[4], Perpetrator = perpetrators[4], Date = new DateTime(2021, 04, 15), Status = true };
            database.Arrests.AddRange(arrests);
            database.SaveChanges();
        }
    }
}
