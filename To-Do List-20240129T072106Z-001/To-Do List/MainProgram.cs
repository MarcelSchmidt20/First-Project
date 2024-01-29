using System;

class Program                                                                           // Program Class
{
    static void Main()                                                                  // Main
    {
        ToDoListe toDoListe = new ToDoListe();                                          // Versuch IDs einzupflegen (WIP)
        int aktuelleAufgabenId = -1;

        while (true)
        {
            Console.WriteLine("\nOptionen:");                                          // Optionenlisten-Optionen
            Console.WriteLine("1. Add Aufgabe");
            Console.WriteLine("2. Zeige Aufgaben");
            Console.WriteLine("3. Aufgabe erledigen");
            Console.WriteLine("4. Aufgabe un-erledigen");
            Console.WriteLine("5. Aufgabe Löschen");
            Console.WriteLine("6. Priority changen");
            Console.WriteLine("7. Change Aufgabenname");
            Console.WriteLine("10. Exit");
            Console.WriteLine("99. Alles clearen");

            Console.Write("Enter Auswahl (1-10):  ");                                  // User Input 1-10
            string choice = Console.ReadLine();                                        // read und process User Input in switch-case below

            switch (choice)
            {
                case "1":
                    Console.Write("Enter Aufgabenbeschreibung: ");                                                                                                  // Aufgabe adden. (Beschreibung) (1/3)
                    string AufgabenBeschreibung = Console.ReadLine();                                                                                               // Priority zuweisen (2/3)
                    Console.Write("Geb die Priority der Aufgabe an. (NotImportant, Average, Wichtig): ");
                    if (Enum.TryParse<AufgabenPrio>(Console.ReadLine(), out AufgabenPrio priority))
                    {
                        Console.Write("Geb das Startdatum der Aufgabe an (Format: dd-MM-yyyy): ");                                                                  // Datum zuweisen start+end (3/3)
                        if (DateTime.TryParseExact(Console.ReadLine(), "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime startTime))       // Parsed Exact time
                        {
                            Console.Write("Geb das Enddatum der Aufgabe an (Format: dd-MM-yyyy): ");
                            if (DateTime.TryParseExact(Console.ReadLine(), "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime endTime))     // Sorgt dafür das Date zugewiesen werden kann
                            {                                                                                                                                       // Und dann gesaved werden kann
                                toDoListe.AddAufgabe(AufgabenBeschreibung, priority, startTime, endTime);
                            }
                            else                                                                                                                                    // Falls falscher Input
                            {
                                Console.WriteLine("Falsches Datumsformat für das Enddatum.");
                            }
                        }

                        else
                        {
                            Console.WriteLine("Falsches Datumsformat für das Startdatum.");                                                                         // Falls falscher Input
                        }
                    }
                    else
                    {
                        Console.WriteLine("Falscher PriorityName. Schreibs richtig xd (NotImportant, Average, Wichtig)");                                           // Falls falscher Priority name (falscher Input)
                    }
                    break;

                case "2":
                    toDoListe.DisplayAufgaben();                                                                                                                    // Greift Methode "DisplayAufgaben" auf, Klasse toDoListe
                    break;

                case "3":
                    Console.Write("Geb die Beschreibung der Aufgabe ein, um sie als erledigt zu markieren:  ");                                                     // Gibt Text aus
                    string markAufgabeCompleted = Console.ReadLine();                                                                                               // Liest User Input
                    toDoListe.MarkAufgabeCompleted(markAufgabeCompleted);                                                                                           // Nutzt Methode "MarkAugabeCompleted" und markiert die Aufgabe
                    break;

                case "4":
                    Console.Write("Geb die Beschreibung der Aufgabe ein, um sie als 'un-erledigt' zu markieren:   ");                                               // Gibt Text aus
                    string uncompleteAufgabe = Console.ReadLine();                                                                                                  // Liest User Input
                    toDoListe.UncompleteAufgabe(uncompleteAufgabe);                                                                                                 // Nutzt Methode "UncompleteAufgabe" und unmarkiert die Aufgabe
                    break;

                case "5":
                    Console.Write("Geb die Aufgabenbeschreibung zum deleten ein:  ");                                                                               // Gibt Text aus
                    string deleteAufgabe = Console.ReadLine();                                                                                                      // Liest User Input
                    toDoListe.DeleteAufgabe(deleteAufgabe);                                                                                                         // Nutzt Methode "DeleteAufgabe" und deleted selected Aufgabe
                    break;

                case "6":
                    Console.Write("Geb die Beschreibung der Aufgabe an to change Priority: ");                                                                      // Gibt Text aus
                    string aufgabenBeschreibung = Console.ReadLine();                                                                                               // Liest User Input

                    Console.Write("Assign eine neue Priority (NotImportant, Average, Wichtig): ");                                                                  // Gibt Text aus
                    if (Enum.TryParse<AufgabenPrio>(Console.ReadLine(), out AufgabenPrio newPriority))                                                              // Parsen des Codes, Überprüfung ob Possible to Parse
                    {                                                                                                                                               // neuer Priority, falls nicht possible -> else
                        toDoListe.ChangeAufgabePriority(aufgabenBeschreibung, newPriority);
                    }
                    else
                    {
                        Console.WriteLine("Falscher PriorityName. Schreibs richtig xd (NotImportant, Average, Wichtig)");                                           // Gibt "falscher User Input" message aus
                    }
                    break;

                case "7":
                    Console.Write("Geb die alte Beschreibung der Aufgabe an, die gechanged werden soll: ");                                                         // Gibt Text aus
                    string alteBeschreibung = Console.ReadLine();                                                                                                   // Liest User Input als "alteBeschreibung"

                    Console.Write("Geb die neue Beschreibung ein:   ");                                                                                             // Gibt Text aus
                    string neueBeschreibung = Console.ReadLine();                                                                                                   // Liest User Input als "neueBeschreibung"

                    toDoListe.ChangeAufgabenName(alteBeschreibung, neueBeschreibung);                                                                               // Nutzt Methode "ChangeAufgabenName"
                    break;                                                                                                                                          // Sorgt dafür das der Name geändert wird

                case "10":  
                    Console.WriteLine("Exite das Programm");                                                                                                        // Gibt Text aus
                    return;                                                                                                                                         // Sorgt zum Exit

                case "99":                                                                                                                                          // Nutzt Methode "DeleteAllAufgaben"
                    toDoListe.DeleteAlleAufgaben();                                                                                                                 // Cleared entire Liste/Text.txt
                    break;

                default:
                    Console.WriteLine("Unmögliche Auswahl, gib eine Nummer zwischen 1-10 ein.");                                                                    // Default output falls wrong User Input
                    break;
            }
        }
    }
}   // . _ - : einpflegen Datum
    // Deadlines