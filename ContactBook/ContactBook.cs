using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook
{
    class ContactBook
    {

        IList<Contact> contacts = new List<Contact>();

        public ContactBook()
        {
            OpenFile();

            string action = "";

            while (action != "quit")
            {
                Console.WriteLine("\nWhat do you want to do?:");
                Console.WriteLine("(add, update, delete, list, save, quit)");

                action = Console.ReadLine().ToLower();

                switch(action)
                {
                    case "add":
                        AddContact();
                        break;

                    case "update":
                        UpdateContact();
                        break;

                    case "delete":
                        DeleteContact();
                        break;

                    case "list":
                        ListContacts();
                        break;

                    case "save":
                        Savefile();
                        break;

                    case "quit":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid input.");
                        break;
                }
            }

        }

            public void OpenFile()
            {
            try { 

                string line;

                var path = Path.Combine(Directory.GetCurrentDirectory(), @".\ContactsBook.txt");

                System.IO.StreamReader file = new System.IO.StreamReader(path);

                while ((line = file.ReadLine()) != null)
                {
                    string[] words = line.Split('|');

                    Contact contact = new Contact();

                    contact.name = words[0];
                    contact.number = words[1];
                    contact.email = words[2];

                    contacts.Add(contact);
                }

                file.Close();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("no Contacts Book file found.");
            }
        }


        public void AddContact()
        {
            Contact contact = new Contact();

            Console.WriteLine("Enter name of contact: ");
            contact.name = Console.ReadLine();

            Console.WriteLine("Enter contact number (press enter to skip): ");
            contact.number = Console.ReadLine();

            Console.WriteLine("Enter contact email (press enter to skip): ");
            contact.email = Console.ReadLine();

            contacts.Add(contact);

            Console.WriteLine(contact.name + " has been added to the contacts book.");
        }

        public void UpdateContact()
        {
            if(contacts.Count == 0)
            {
                Console.WriteLine("You got no contacts here. Try adding someone, first.");

                return;
            }
            Console.WriteLine("Enter name of contact to update: ");
            string name = Console.ReadLine();
            Boolean checker = true;

            foreach( Contact c in contacts)
            {
                if( name.Equals(c.name) )
                {
                    checker = false;

                    Console.WriteLine("Enter '1' to update contact name ");
                    Console.WriteLine("Enter '2' to update contact number ");
                    Console.WriteLine("Enter '3' to update contact email ");

                    string ans = Console.ReadLine();

                    switch (ans)
                    {
                        case "1":
                            Console.WriteLine("Enter new name: ");
                            string newName = Console.ReadLine();
                            c.name = newName;
                            break;

                        case "2":
                            Console.WriteLine("Enter a new number: ");
                            string newNumber = Console.ReadLine();
                            c.number = newNumber;
                            break;

                        case "3":
                            Console.WriteLine("Enter a new email: ");
                            string newEmail = Console.ReadLine();
                            c.email = newEmail;
                            break;

                        default:
                            Console.WriteLine(ans + " is not a valid input :(");
                            break;
                    }
                    break;
                }
            }
            if(checker)
            {
               Console.WriteLine(name + " is not in the contacts book. Add contact?");

               string ans = Console.ReadLine().ToLower();

                switch (ans)
                {
                    case "yes":
                        AddNamedContact(name);
                        break;

                    case "y":
                        AddNamedContact(name);
                        break;

                    case "no":
                        Console.WriteLine("Yeah, good call.");
                        break;

                    case "n":
                        Console.WriteLine("Yeah, good call.");
                        break;

                    default:
                        Console.WriteLine("Nope, sorry, didn't understand that.");
                        break;
                }
            }

        }

        private void AddNamedContact(string name)
        {
            Contact contact = new Contact();

            contact.name = name;

            Console.WriteLine("Enter contact number (press enter to skip): ");
            contact.number = Console.ReadLine();

            Console.WriteLine("Enter contact email (press enter to skip): ");
            contact.email = Console.ReadLine();

            contacts.Add(contact);

            Console.WriteLine(name + " has been added to the contacts book.");
        }

        public void ListContacts()
        {
            if (contacts.Count == 0)
            {
                Console.WriteLine("Seems like you got no contacts. Yikes.");
                Console.WriteLine("(press any key to continue...)");
                Console.ReadKey();
                return;
            }
            foreach( Contact c in contacts)
            {
                PrintContacts(c);
            }
            Console.WriteLine("(press any key to continue...)");
            Console.ReadKey();
        }

        private void PrintContacts(Contact contact)
        {
            Console.WriteLine("Name: " + contact.name);
            Console.WriteLine("Number: " + contact.number);
            Console.WriteLine("Email: " + contact.email);
            Console.WriteLine("=============================");
        }

        private void PrintContactNames()
        {
            int index = 1;

            foreach(Contact c in contacts)
            {
                Console.WriteLine(index + ": " + c.name);
                    index++;
            }
        }

        public void DeleteContact()
        {
            if (contacts.Count == 0)
            {
                Console.WriteLine("You got no contacts here. Try adding someone, first.");

                return;
            }
            Console.WriteLine("Enter index of contact to delete:");

            PrintContactNames();

            int num = Convert.ToInt32(Console.ReadLine());

            int index = num - 1;

            contacts.RemoveAt(index);

            Console.WriteLine("Contact deleted.");
        }

        public void Savefile()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), @".\ContactsBook.txt");

            using (TextWriter tw = new StreamWriter(path))
            {
                foreach (Contact contact in contacts)
                {
                    tw.Write(contact.name + "|");

                    if(contact.number != "")
                    {
                        tw.Write(contact.number + "|");
                    }
                    if(contact.email != "")
                    {
                        tw.Write(contact.email);
                    }
                    tw.Write("\n");
                }
            }
            Console.Write("Contacts saved to txt file!");
        }

        static void Main(string[] args)
        {
            new ContactBook();
        }
    }
}
