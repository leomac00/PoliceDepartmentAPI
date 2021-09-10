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
            var u0 = new User() { Name = "Lulay", CPF = "234.567.800-99", RegisterId = "lulay", UserRole = "Judge", Status = true, Password = "$2b$10$XRyEC0dLHCZizL5sudXqcucXBXEMxUHmmwcXDLrqqQm8Csv.C61Dq" }; //Password = gft2021
            var u1 = new User() { Name = "Advogado Dredd", CPF = "098.756.543-21", RegisterId = "i am the law", UserRole = "Lawyer", Status = true, Password = "$2b$10$xE28cZ98TXk47pG46HwBtumfJ.hFSFg6cc0fgY990oYY0.uRLawLW" }; //Password = stalone
            database.Users.AddRange(u0, u1); //adding Lulay(uid="lulay"; pw="gft2021") and Advogado Dredd(uid="i am the law"; pw="stalone") to DB
            database.SaveChanges();

            //Perpetrators (Criminosos)
            var per0 = new Perpetrator() { Name = "Jair Messias Bolsonaro", CPF = "666.666.666-66", Status = true };
            var per1 = new Perpetrator() { Name = "Chico Picadinho", CPF = "111.111.111-11", Status = true };
            var per2 = new Perpetrator() { Name = "Joao de Deus", CPF = "222.222.222-22", Status = true };
            var per3 = new Perpetrator() { Name = "Pedrinho Matador", CPF = "333.333.333-33", Status = true };
            var per4 = new Perpetrator() { Name = "Lazaro", CPF = "444.444.444-44", Status = true };
            database.Perpetrators.AddRange(per0, per1, per2, per3, per4);
            database.SaveChanges();

            //Victims
            var vic0 = new Victim() { Name = "Cher", CPF = "000.000.000-00", Status = true };
            var vic1 = new Victim() { Name = "Joao Bosco", CPF = "123.123.123-12", Status = true };
            var vic2 = new Victim() { Name = "Tom do Jerry", CPF = "234.234.234-23", Status = true };
            var vic3 = new Victim() { Name = "Sebastiana Bastiana", CPF = "345.345.345-34", Status = true };
            var vic4 = new Victim() { Name = "Uzumaki Naruto", CPF = "456.456.456-45", Status = true };
            database.Victims.AddRange(vic0, vic1, vic2, vic3, vic4);
            database.SaveChanges();

            //Addresses
            var adr0 = new Adress() { Street = "Rua 5-E", Number = "10", City = "Maceió", State = "AL", ZIPCode = "57084-632", Status = true };
            var adr1 = new Adress() { Street = "Rua Adelina Bastos", Number = "11", City = "Ilhéus", State = "BA", ZIPCode = "45654-030", Status = true };
            var adr2 = new Adress() { Street = "Rua Adelina Bastos", Number = "12", City = "Ilhéus", State = "BA", ZIPCode = "45654-030", Status = true };
            var adr3 = new Adress() { Street = "Quadra QNP 23", Number = "13", City = "Brasília", State = "DF", ZIPCode = "72242-050", Status = true };
            var adr4 = new Adress() { Street = "Rua Otoniel Menezes", Number = "14", City = "Natal", State = "RN", ZIPCode = "59010-510", Status = true };
            database.Adresses.AddRange(adr0, adr1, adr2, adr3, adr4);
            database.SaveChanges();

            //PoliceDepartments (Delegacias)
            var pd0 = new PoliceDepartment() { Adress = adr0, PhoneNumber = "1111-1111", Name = "Departamento Bacana", Status = true };
            var pd1 = new PoliceDepartment() { Adress = adr1, PhoneNumber = "2222-2222", Name = "Departamento Supimpa", Status = true };
            var pd2 = new PoliceDepartment() { Adress = adr2, PhoneNumber = "3333-3333", Name = "Departamento do Geremias", Status = true };
            var pd3 = new PoliceDepartment() { Adress = adr3, PhoneNumber = "4444-4444", Name = "Departamento Gonzaguinha", Status = true };
            var pd4 = new PoliceDepartment() { Adress = adr4, PhoneNumber = "5555-5555", Name = "Departamento Linkin Park", Status = true };
            database.PoliceDepartments.AddRange(pd0, pd1, pd2, pd3, pd4);
            database.SaveChanges();

            //Deputies (Delegados)
            var dep0 = new Deputy() { Name = "Jorge Papareli", CPF = "567.567.567-56", PoliceDepartment = pd0, Shift = "Morning", RegisterId = "123jorge", Status = true };
            var dep1 = new Deputy() { Name = "Joao da Silva", CPF = "678.687.678-67", PoliceDepartment = pd1, Shift = "Evening", RegisterId = "123joao", Status = true };
            var dep2 = new Deputy() { Name = "Anderson Silva", CPF = "789.789.789-78", PoliceDepartment = pd2, Shift = "Night", RegisterId = "123anderson", Status = true };
            var dep3 = new Deputy() { Name = "Zebedeu Carmillo", CPF = "890.890.890-89", PoliceDepartment = pd3, Shift = "Morning", RegisterId = "123zebedeu", Status = true };
            var dep4 = new Deputy() { Name = "Vlad Tepes II", CPF = "123.321.123-32", PoliceDepartment = pd4, Shift = "Evening", RegisterId = "123vlad", Status = true };
            database.Deputies.AddRange(dep0, dep1, dep2, dep3, dep4);
            database.SaveChanges();

            //PoliceOfficers (Policiais)
            var po0 = new PoliceOfficer() { Name = "Capitao Tanaka", CPF = "234.432.234-43", RegisterId = "123tanaka", Rank = "Police captain", Status = true };
            var po1 = new PoliceOfficer() { Name = "Guarda Belo", CPF = "345.543.345-54", RegisterId = "123Belo", Rank = "Police officer/patrol officer/police detective", Status = true };
            var po2 = new PoliceOfficer() { Name = "John McClane", CPF = "456.654.456-65", RegisterId = "123john", Rank = "Police corporal", Status = true };
            var po3 = new PoliceOfficer() { Name = "Harvey Dent", CPF = "567.765.567-76", RegisterId = "123harvey", Rank = "Chief of police", Status = true };
            var po4 = new PoliceOfficer() { Name = "John Rambo", CPF = "678.876.678-87", RegisterId = "123rambo", Rank = "Police lieutenant", Status = true };
            database.PoliceOfficers.AddRange(po0, po1, po2, po3, po4);
            database.SaveChanges();

            //Coroners (Legistas)
            var cor0 = new Coroner() { Name = "Amanda Baiao", CPF = "908.098.098-09", RegisterId = "123baiao", Status = true };
            var cor1 = new Coroner() { Name = "Joao Alecrim", CPF = "987.987.987-98", RegisterId = "123alecrim", Status = true };
            var cor2 = new Coroner() { Name = "Orochimaru Azul", CPF = "876.876.876-87", RegisterId = "123orochimaru", Status = true };
            var cor3 = new Coroner() { Name = "Vegeta da Silva", CPF = "765.765.765-76", RegisterId = "123vegeta", Status = true };
            var cor4 = new Coroner() { Name = "Bruce Mandioca", CPF = "654.654.654-65", RegisterId = "123mandioca", Status = true };
            database.Coroners.AddRange(cor0, cor1, cor2, cor3, cor4);
            database.SaveChanges();

            //Crimes (Crimes)
            var cri0 = new Crime() { Perpetrator = per0, Victim = vic0, Date = new DateTime(2016, 01, 10), Description = "The Perpetrator tried to blow up the victim´s house", Adress = adr0, Status = true };
            var cri1 = new Crime() { Perpetrator = per1, Victim = vic1, Date = new DateTime(2017, 02, 11), Description = "The perpetrator tried to steal victim´s phone", Adress = adr1, Status = true };
            var cri2 = new Crime() { Perpetrator = per2, Victim = vic2, Date = new DateTime(2018, 03, 12), Description = "The perpetrator tried to injure the victim", Adress = adr2, Status = true };
            var cri3 = new Crime() { Perpetrator = per3, Victim = vic3, Date = new DateTime(2019, 04, 13), Description = "The perpetrator tried to steal the victim´s dog", Adress = adr3, Status = true };
            var cri4 = new Crime() { Perpetrator = per4, Victim = vic4, Date = new DateTime(2020, 04, 14), Description = "The perpetrator stole victim´s bag", Adress = adr4, Status = true };
            database.Crimes.AddRange(cri0, cri1, cri2, cri3, cri4);
            database.SaveChanges();

            //Autopsies (Autopsias)
            var aut0 = new Autopsy() { Victim = vic0, Coroner = cor0, Date = new DateTime(2021, 07, 25), Description = "The autopsy went fine", Status = true };
            var aut1 = new Autopsy() { Victim = vic1, Coroner = cor1, Date = new DateTime(2021, 07, 26), Description = "The autopsy went ok", Status = true };
            var aut2 = new Autopsy() { Victim = vic2, Coroner = cor2, Date = new DateTime(2021, 07, 27), Description = "The autopsy went meh", Status = true };
            var aut3 = new Autopsy() { Victim = vic3, Coroner = cor3, Date = new DateTime(2021, 07, 28), Description = "The autopsy went wrong", Status = true };
            var aut4 = new Autopsy() { Victim = vic4, Coroner = cor4, Date = new DateTime(2021, 07, 29), Description = "The autopsy went very wrong, THE CORPSE IS ALIVE!", Status = true };
            database.Autopsies.AddRange(aut0, aut1, aut2, aut3, aut4);
            database.SaveChanges();

            //Arrest (Prisões)
            var arr0 = new Arrest() { Officer = po0, Deputy = dep0, Crime = cri0, Perpetrator = per0, Date = new DateTime(2021, 04, 15), Status = true };
            var arr1 = new Arrest() { Officer = po1, Deputy = dep1, Crime = cri1, Perpetrator = per1, Date = new DateTime(2021, 04, 15), Status = true };
            var arr2 = new Arrest() { Officer = po2, Deputy = dep2, Crime = cri2, Perpetrator = per2, Date = new DateTime(2021, 04, 15), Status = true };
            var arr3 = new Arrest() { Officer = po3, Deputy = dep3, Crime = cri3, Perpetrator = per3, Date = new DateTime(2021, 04, 15), Status = true };
            var arr4 = new Arrest() { Officer = po4, Deputy = dep4, Crime = cri4, Perpetrator = per4, Date = new DateTime(2021, 04, 15), Status = true };
            database.Arrests.AddRange(arr0, arr1, arr2, arr3, arr4);
            database.SaveChanges();
        }
    }
}
