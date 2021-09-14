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
            Perpetrator[] perpetrators = new Perpetrator[8];
            perpetrators[0] = new Perpetrator() { Name = "Bandido da Luz vermelha", CPF = "666.666.666-66", Status = true };
            perpetrators[1] = new Perpetrator() { Name = "Chico Picadinho", CPF = "111.111.111-11", Status = true };
            perpetrators[2] = new Perpetrator() { Name = "Joao de Deus", CPF = "222.222.222-22", Status = true };
            perpetrators[3] = new Perpetrator() { Name = "Pedrinho Matador", CPF = "333.333.333-33", Status = true };
            perpetrators[4] = new Perpetrator() { Name = "Lazaro", CPF = "444.444.444-44", Status = true };
            perpetrators[5] = new Perpetrator() { Name = "Humberto Zablinsk", CPF = "444.777.444-77", Status = true };
            perpetrators[6] = new Perpetrator() { Name = "Tiao Marretao", CPF = "223.444.111-44", Status = true };
            perpetrators[7] = new Perpetrator() { Name = "Zezinho Pardal", CPF = "345.444.784-44", Status = true };
            database.Perpetrators.AddRange(perpetrators);
            database.SaveChanges();

            //Victims
            Victim[] victims = new Victim[8];
            victims[0] = new Victim() { Name = "Cher", CPF = "000.000.000-00", Status = true };
            victims[1] = new Victim() { Name = "Joao Bosco", CPF = "123.123.123-12", Status = true };
            victims[2] = new Victim() { Name = "Tom do Jerry", CPF = "234.234.234-23", Status = true };
            victims[3] = new Victim() { Name = "Sebastiana Bastiana", CPF = "345.345.345-34", Status = true };
            victims[4] = new Victim() { Name = "Uzumaki Naruto", CPF = "456.456.456-45", Status = true };
            victims[5] = new Victim() { Name = "Uchirra Itachi", CPF = "125.412.456-45", Status = true };
            victims[6] = new Victim() { Name = "Haruno Sakura", CPF = "654.456.456-45", Status = true };
            victims[7] = new Victim() { Name = "Son Goku", CPF = "456.964.423-45", Status = true };
            database.Victims.AddRange(victims);
            database.SaveChanges();

            //Addresses
            Adress[] adresses = new Adress[8];
            adresses[0] = new Adress() { Street = "Rua 5-E", Number = "10", City = "Maceió", State = "AL", ZIPCode = "57084-632", Status = true };
            adresses[1] = new Adress() { Street = "Rua Adelina Bastos", Number = "11", City = "Ilhéus", State = "BA", ZIPCode = "45654-030", Status = true };
            adresses[2] = new Adress() { Street = "Rua Adelina Bastos", Number = "12", City = "Ilhéus", State = "BA", ZIPCode = "45654-030", Status = true };
            adresses[3] = new Adress() { Street = "Quadra QNP 23", Number = "13", City = "Brasília", State = "DF", ZIPCode = "72242-050", Status = true };
            adresses[4] = new Adress() { Street = "Rua Otoniel Menezes", Number = "14", City = "Natal", State = "RN", ZIPCode = "59010-510", Status = true };
            adresses[5] = new Adress() { Street = "Rua Rio Jari", Number = "15", City = "Pinhais", State = "PR", ZIPCode = "83322-500", Status = true };
            adresses[6] = new Adress() { Street = "Rua Joana Angélica", Number = "16", City = "Teresina", State = "PI", ZIPCode = "64006-295", Status = true };
            adresses[7] = new Adress() { Street = "Avenida Floriano Gonçalves de Lima 93", Number = "17", City = "Xexéu", State = "PE", ZIPCode = "55555-970", Status = true };
            database.Adresses.AddRange(adresses);
            database.SaveChanges();

            //PoliceDepartments = "PDs" (Delegacias)
            PoliceDepartment[] PDs = new PoliceDepartment[8];
            PDs[0] = new PoliceDepartment() { Adress = adresses[0], PhoneNumber = "1111-1111", Name = "Departamento Bacana", Status = true };
            PDs[1] = new PoliceDepartment() { Adress = adresses[1], PhoneNumber = "2222-2222", Name = "Departamento Supimpa", Status = true };
            PDs[2] = new PoliceDepartment() { Adress = adresses[2], PhoneNumber = "3333-3333", Name = "Departamento do Geremias", Status = true };
            PDs[3] = new PoliceDepartment() { Adress = adresses[3], PhoneNumber = "4444-4444", Name = "Departamento Gonzaguinha", Status = true };
            PDs[4] = new PoliceDepartment() { Adress = adresses[4], PhoneNumber = "5555-5555", Name = "Departamento Linkin Park", Status = true };
            PDs[5] = new PoliceDepartment() { Adress = adresses[5], PhoneNumber = "1234-5555", Name = "Departamento Bicoca Diagonal", Status = true };
            PDs[6] = new PoliceDepartment() { Adress = adresses[6], PhoneNumber = "2345-5555", Name = "Departamento Rivendell", Status = true };
            PDs[7] = new PoliceDepartment() { Adress = adresses[7], PhoneNumber = "3456-5555", Name = "Departamento Zaibatsu", Status = true };
            database.PoliceDepartments.AddRange(PDs);
            database.SaveChanges();

            //Deputies (Delegados)
            Deputy[] deputies = new Deputy[8];
            deputies[0] = new Deputy() { Name = "Jorge Papareli", CPF = "567.567.567-56", PoliceDepartment = PDs[0], Shift = "Morning", RegisterId = "123jorge", Status = true };
            deputies[1] = new Deputy() { Name = "Joao da Silva", CPF = "678.687.678-67", PoliceDepartment = PDs[1], Shift = "Evening", RegisterId = "123joao", Status = true };
            deputies[2] = new Deputy() { Name = "Anderson Silva", CPF = "789.789.789-78", PoliceDepartment = PDs[2], Shift = "Night", RegisterId = "123anderson", Status = true };
            deputies[3] = new Deputy() { Name = "Zebedeu Carmillo", CPF = "890.890.890-89", PoliceDepartment = PDs[3], Shift = "Morning", RegisterId = "123zebedeu", Status = true };
            deputies[4] = new Deputy() { Name = "Vlad Tepes II", CPF = "123.321.123-32", PoliceDepartment = PDs[4], Shift = "Evening", RegisterId = "123vlad", Status = true };
            deputies[5] = new Deputy() { Name = "Bruce Wayne", CPF = "223.321.123-32", PoliceDepartment = PDs[5], Shift = "Night", RegisterId = "123wayne", Status = true };
            deputies[6] = new Deputy() { Name = "Leonel Bezerra", CPF = "122.321.123-32", PoliceDepartment = PDs[6], Shift = "Morning", RegisterId = "123bezerra", Status = true };
            deputies[7] = new Deputy() { Name = "Sergio Palmares", CPF = "123.322.123-32", PoliceDepartment = PDs[7], Shift = "Evening", RegisterId = "123palms", Status = true };
            database.Deputies.AddRange(deputies);
            database.SaveChanges();

            //PoliceOfficers = "POs" (Policiais)
            PoliceOfficer[] POs = new PoliceOfficer[8];
            POs[0] = new PoliceOfficer() { Name = "Capitao Tanaka", CPF = "234.432.234-43", RegisterId = "123tanaka", Rank = "Police captain", Status = true };
            POs[1] = new PoliceOfficer() { Name = "Guarda Belo", CPF = "345.543.345-54", RegisterId = "123Belo", Rank = "Police officer/patrol officer/police detective", Status = true };
            POs[2] = new PoliceOfficer() { Name = "John McClane", CPF = "456.654.456-65", RegisterId = "123john", Rank = "Police corporal", Status = true };
            POs[3] = new PoliceOfficer() { Name = "Harvey Dent", CPF = "567.765.567-76", RegisterId = "123harvey", Rank = "Chief of police", Status = true };
            POs[4] = new PoliceOfficer() { Name = "John Rambo", CPF = "678.812.678-87", RegisterId = "123rambo", Rank = "Police lieutenant", Status = true };
            POs[5] = new PoliceOfficer() { Name = "John Wick", CPF = "678.646.678-87", RegisterId = "123wick", Rank = "Police corporal", Status = true };
            POs[6] = new PoliceOfficer() { Name = "John Wayne", CPF = "678.824.678-87", RegisterId = "123jwayne", Rank = "Police captain", Status = true };
            POs[7] = new PoliceOfficer() { Name = "John Constantine", CPF = "678.194.678-87", RegisterId = "123constantine", Rank = "Police sergeant", Status = true };
            database.PoliceOfficers.AddRange(POs);
            database.SaveChanges();

            //Coroners (Legistas)
            Coroner[] coroners = new Coroner[8];
            coroners[0] = new Coroner() { Name = "Amanda Baiao", CPF = "908.098.098-09", RegisterId = "123baiao", Status = true };
            coroners[1] = new Coroner() { Name = "Joao Alecrim", CPF = "987.987.987-98", RegisterId = "123alecrim", Status = true };
            coroners[2] = new Coroner() { Name = "Orochimaru Nitzschke", CPF = "876.876.876-87", RegisterId = "123orochimaru", Status = true };
            coroners[3] = new Coroner() { Name = "Vegeta da Silva", CPF = "765.765.765-76", RegisterId = "123vegeta", Status = true };
            coroners[4] = new Coroner() { Name = "DIO", CPF = "654.654.654-65", RegisterId = "123dio", Status = true };
            coroners[5] = new Coroner() { Name = "Bruce Dickinson", CPF = "624.654.624-65", RegisterId = "123dickinson", Status = true };
            coroners[6] = new Coroner() { Name = "Humberto Gessinger", CPF = "652.224.654-65", RegisterId = "123gessinger", Status = true };
            coroners[7] = new Coroner() { Name = "Fred Mercury", CPF = "654.613.614-22", RegisterId = "123mercury", Status = true };
            database.Coroners.AddRange(coroners);
            database.SaveChanges();

            //Crimes (Crimes)
            Crime[] crimes = new Crime[8];
            crimes[0] = new Crime() { Perpetrator = perpetrators[0], Victim = victims[0], Date = new DateTime(2016, 01, 10), Description = "The Perpetrator tried to blow up the victim´s house", Adress = adresses[0], Status = true };
            crimes[1] = new Crime() { Perpetrator = perpetrators[1], Victim = victims[1], Date = new DateTime(2017, 02, 11), Description = "The perpetrator tried to steal victim´s phone", Adress = adresses[1], Status = true };
            crimes[2] = new Crime() { Perpetrator = perpetrators[2], Victim = victims[2], Date = new DateTime(2018, 03, 12), Description = "The perpetrator tried to injure the victim", Adress = adresses[2], Status = true };
            crimes[3] = new Crime() { Perpetrator = perpetrators[3], Victim = victims[3], Date = new DateTime(2019, 04, 13), Description = "The perpetrator tried to steal the victim´s dog", Adress = adresses[3], Status = true };
            crimes[4] = new Crime() { Perpetrator = perpetrators[4], Victim = victims[4], Date = new DateTime(2020, 04, 14), Description = "The perpetrator stole victim´s bag", Adress = adresses[4], Status = true };
            crimes[5] = new Crime() { Perpetrator = perpetrators[5], Victim = victims[5], Date = new DateTime(2020, 04, 15), Description = "The perpetrator tried to harm the victim´s dog", Adress = adresses[5], Status = true };
            crimes[6] = new Crime() { Perpetrator = perpetrators[6], Victim = victims[6], Date = new DateTime(2020, 04, 16), Description = "The perpetrator stupidly hit the victim´s wind shield", Adress = adresses[6], Status = true };
            crimes[7] = new Crime() { Perpetrator = perpetrators[7], Victim = victims[7], Date = new DateTime(2020, 04, 17), Description = "The perpetrator jumped the victim´s wall", Adress = adresses[7], Status = true };
            database.Crimes.AddRange(crimes);
            database.SaveChanges();

            //Autopsies (Autopsias)
            Autopsy[] autopsies = new Autopsy[8];
            autopsies[0] = new Autopsy() { Victim = victims[0], Coroner = coroners[0], Date = new DateTime(2021, 07, 25), Description = "The autopsy went fine", Status = true };
            autopsies[1] = new Autopsy() { Victim = victims[1], Coroner = coroners[1], Date = new DateTime(2021, 07, 26), Description = "The autopsy went ok", Status = true };
            autopsies[2] = new Autopsy() { Victim = victims[2], Coroner = coroners[2], Date = new DateTime(2021, 07, 27), Description = "The autopsy went meh", Status = true };
            autopsies[3] = new Autopsy() { Victim = victims[3], Coroner = coroners[3], Date = new DateTime(2021, 07, 28), Description = "The autopsy went wrong", Status = true };
            autopsies[4] = new Autopsy() { Victim = victims[4], Coroner = coroners[4], Date = new DateTime(2021, 07, 29), Description = "The autopsy went very wrong, THE CORPSE IS ALIVE!", Status = true };
            autopsies[5] = new Autopsy() { Victim = victims[5], Coroner = coroners[5], Date = new DateTime(2021, 07, 30), Description = "The autopsy wasn´t performed", Status = true };
            autopsies[6] = new Autopsy() { Victim = victims[6], Coroner = coroners[6], Date = new DateTime(2021, 07, 31), Description = "The autopsy was ok", Status = true };
            autopsies[7] = new Autopsy() { Victim = victims[7], Coroner = coroners[7], Date = new DateTime(2021, 08, 01), Description = "The autopsy made clear that the victim is dead", Status = true };
            database.Autopsies.AddRange(autopsies);
            database.SaveChanges();

            //Arrest (Prisões)
            Arrest[] arrests = new Arrest[8];
            arrests[0] = new Arrest() { Officer = POs[0], Deputy = deputies[0], Crime = crimes[0], Perpetrator = perpetrators[0], Date = new DateTime(2021, 04, 15), Status = true };
            arrests[1] = new Arrest() { Officer = POs[1], Deputy = deputies[1], Crime = crimes[1], Perpetrator = perpetrators[1], Date = new DateTime(2021, 04, 15), Status = true };
            arrests[2] = new Arrest() { Officer = POs[2], Deputy = deputies[2], Crime = crimes[2], Perpetrator = perpetrators[2], Date = new DateTime(2021, 04, 15), Status = true };
            arrests[3] = new Arrest() { Officer = POs[3], Deputy = deputies[3], Crime = crimes[3], Perpetrator = perpetrators[3], Date = new DateTime(2021, 04, 15), Status = true };
            arrests[4] = new Arrest() { Officer = POs[4], Deputy = deputies[4], Crime = crimes[4], Perpetrator = perpetrators[4], Date = new DateTime(2021, 04, 16), Status = true };
            arrests[5] = new Arrest() { Officer = POs[5], Deputy = deputies[5], Crime = crimes[5], Perpetrator = perpetrators[5], Date = new DateTime(2021, 04, 17), Status = true };
            arrests[6] = new Arrest() { Officer = POs[6], Deputy = deputies[6], Crime = crimes[6], Perpetrator = perpetrators[6], Date = new DateTime(2021, 04, 18), Status = true };
            arrests[7] = new Arrest() { Officer = POs[7], Deputy = deputies[7], Crime = crimes[7], Perpetrator = perpetrators[7], Date = new DateTime(2021, 04, 19), Status = true };
            database.Arrests.AddRange(arrests);
            database.SaveChanges();
        }
    }
}
