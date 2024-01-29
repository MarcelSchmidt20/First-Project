using System;
using System.Collections.Generic;                                                                                  // Macht es possible für die Datei/Compiler die "using-directives/direktiven" zu nutzen
using System.IO;                                                                                                   // Viele Funktionen und Typen der .NET-Library z.b Input/Output und die ganzen Listen/Klassen

class ToDoListe                                                                                                    // Klasse "ToDoListe" wird definiert
{
    private List<AufgabenItem> Aufgaben = new List<AufgabenItem>();                                                // Klasse enthält Liste von "AufgabenItem-Objekten"
    private string DateiPathAufgabe = "text.txt";                                                                  // Wird als Pfad zur File verwendet um zu saven und laden
    private static int AufgabenIDCounter = 0;                                                                      // Statischer Zähler (WIP)
    public static int GenerateUniqueId()                                                                           // Statische Methode für IDs (WIP) 
    {                                                                                                              // Code dient zum verwalten der Liste mit Aufgaben
        return ++AufgabenIDCounter;                                                     
    }

    public ToDoListe()                                                                                              // Constructor der ToDoListe-Class ruft Methode "LoadAufgabeVonDatei"
    {                                                                                                               // Versucht beim erstellen einer Instanz/starten des Programms, vorhandene Aufgaben aus einer Datei zu laden
        LoadAufgabeVonDatei();
    }

    public void AddAufgabe(string beschreibung, AufgabenPrio priority, DateTime startZeit, DateTime endZeit)        // Methode fügt eine neue Aufgabe zur Liste "Aufgaben" hinzu
    {
        int id = GenerateUniqueId();                                                                                // ID wird generated
        AufgabenItem newAuf = new AufgabenItem(id, beschreibung, priority, startZeit, endZeit);                     // Aufgabe wird mit den übergebenen Parametern created. 
        Aufgaben.Add(newAuf);                                                                                       // Zur Liste geadded 
        Console.WriteLine($"Aufgabe added: (ID: {newAuf.ID}): {beschreibung}, Priority {priority}, Zeitraum: {startZeit} bis {endZeit}"); // Dann ausgegeben als Text
        SaveAufgabeAlsDatei();                                                                                                            // Und finally gespeichert
    }

    public void DisplayAufgaben()                                                                                   // Methode zum Anzeigen aller Aufgaben
    {
        Console.Clear();                                                                                            // Cleared vorm Anzeigen einmal die Console for the looks
        Console.WriteLine("");                                                                                      // Unused, Ist basically nen "Title" des DisplayAufgaben ist jtz einfach nur Blankspace

        foreach (var task in Aufgaben)                                                                              // foreach Schleife/foreach Loop
        {                                                                                                           // Durchläuft jede Aufgabe in der Liste Aufgaben
            Console.WriteLine(new string('*', Console.WindowWidth - 60));                                           // Sorgt für die * zwischen den Aufgaben und passt sich an das Fenster an, for the looks basically

            Console.ForegroundColor = task.IstDone ? ConsoleColor.Green : ConsoleColor.Red;                         // Text wird Grün gemacht wenn true, sonst Red wenn false

            Console.Write($"{task.Beschreibung} - Status: {(task.IstDone ? "fertig" : "Nicht fertig")}");           // Ausgabe Text

            Console.ResetColor();                                                                                   // Textcolor wird resetted, damit nicht alles weirdly colored ist
            // Console.WriteLine();

           

            switch (task.Priority)                                                                                  // Hier wird je nach Priority eine Farbe gesetzt.
            {
                case AufgabenPrio.NotImportant:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case AufgabenPrio.Average:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case AufgabenPrio.Wichtig:
                    Console.ForegroundColor = ConsoleColor.DarkRed;                                                 // NotImportant Hellblau, Average Geld, Wichtig Dunkelrot
                    break;
            }
            Console.WriteLine($", Priority: {task.Priority}");                                                      // Die Priority der Aufgabe wird auf der Console ausgegeben
            Console.ResetColor();                                                                                   // Wieder Farbe zum Standard resetten, damit es nicht fked ist

            if (task.StartZeit.HasValue)                                                                            // if checked ob StartZeit der Aufgabe einen Wert hat
            {
                string coloredStartTime = $"Startdatum: {task.StartZeit.Value.ToShortDateString()}";                // Hier wird StringChain erstellt, die das Startdatum der Aufgabe beinhaltet
                Console.ForegroundColor = ConsoleColor.DarkMagenta;                                                 // Textfarbe auf DunkelMagenta gesetzt
                Console.Write($" {coloredStartTime}");                                                              // Gefärbtes Startdatum in der Console ausgegeben
                Console.ResetColor();                                                                               // Farbe resetted back to default
            }
            if (task.EndZeit.HasValue)                                                                              // if checked ob EndZeit der Aufgabe einen Wert hat
            {
                string coloredEndTime = $"Enddatum: {task.EndZeit.Value.ToShortDateString()}";                      // Hier wird StringChain erstellt, die das EndDatum der Aufgabe beinhaltet
                Console.ForegroundColor = ConsoleColor.DarkCyan;                                                    // Textfarbe auf "DarkCyan" gestetzt
                Console.Write($" {coloredEndTime}");                                                                // Gefärbtes Enddatum in der Console ausgegeben
                Console.ResetColor();                                                                               // Farbe resetted back to default
            }

            Console.WriteLine();                                                                                    // Wird dann alles in die Console ausgegeben
        }
    }



    public void MarkAufgabeCompleted(string beschreibung)                                                                       // Methode markiert Aufgabe als abgeschlossen
    {
        AufgabenItem task = Aufgaben.Find(t => t.Beschreibung.Equals(beschreibung, StringComparison.OrdinalIgnoreCase));        // Aufgabe mit angegebener Beschreibung wird gesucht, nicht case-sensitive weil StringComparison.OrdinalIgnoreCase used wird

        if (task != null)                                                                                                       // Wird gechecked ob die Aufgabe gefunden wird
        {
            task.IstDone = true;                                                                                                // Wenn gefunden, wird IstDone auf True gesetzt.
            Console.WriteLine($"Aufgabe als completed markiert: {beschreibung}");                                               // Zeigt dann an, dass die Aufgabe completed ist
            SaveAufgabeAlsDatei();                                                                                              // Saved es in text.txt
        }
        else                                                                                                                    // Falls not found, wird eine Message in Console ausgegeben. (Wrong User Input)
        {
            Console.WriteLine($"Aufgaben nicht gefunden: {beschreibung}");
        }
    }


    public void UncompleteAufgabe(string beschreibung)                                                                          // Methode markiert Aufgabe als uncompleted/nicht abgeschlossen
    {
        AufgabenItem task = Aufgaben.Find(t => t.Beschreibung.Equals(beschreibung, StringComparison.OrdinalIgnoreCase));        // Aufgabe mit angegebener Beschreibung wird gesucht, nicht case-sensitive weil StringComparison.OrdinalIgnoreCase used wird

        if (task != null)                                                                                                       // Wird gechecked ob Aufgabe gefunden wird
        {
            task.IstDone = false;                                                                                               // Wenn gefunden, wird IstDone auf false gesetzt.
            Console.WriteLine($"Aufgabe wurde als nicht finished markiert: {beschreibung}");                                    // Zeigt dann an, dass die Aufgabe nicht mehr completed ist
            SaveAufgabeAlsDatei();                                                                                              // Saved es in text.txt
        }
    }

    public void DeleteAufgabe(string beschreibung)                                                                              // Methode deleted Aufgabe
    {
        AufgabenItem task = Aufgaben.Find(t => t.Beschreibung.Equals(beschreibung, StringComparison.OrdinalIgnoreCase));        // Aufgabe mit angegebener Beschreibung wird gesucht, nicht case-sensitive weil StringComparison.OrdinalIgnoreCase used wird

        if (task != null)                                                                                                       // Wird gechecked ob Aufgabe gefunden wird
        {
            Aufgaben.Remove(task);                                                                                              // Wenn gefunden, wird benannte Aufgabe aus der Aufgaben-Liste deleted
            Console.WriteLine($"Aufgabe deleted: {beschreibung}");
            SaveAufgabeAlsDatei();
        }
        else                                                                                                                    // Falls nicht found, wird Message in Console ausgegeben. (Wrong User Input)
        {
            Console.WriteLine($"Aufgabe nicht gefunden: {beschreibung}");
        }
    }

    public void ChangeAufgabePriority(string aufgabenBeschreibung, AufgabenPrio newPriority)                                    // Methode changed Priorities der Aufgabe
    {
        AufgabenItem task = Aufgaben.Find(t => t.Beschreibung.Equals(aufgabenBeschreibung, StringComparison.OrdinalIgnoreCase));// Aufgabe mit angegebener Beschreibung wird gesucht, nicht case-sensitive weil StringComparison.OrdinalIgnoreCase used wird    

        if (task != null)                                                                                                       // Wird gechecked ob Aufgabe gefunden wird
        {
            task.Priority = newPriority;                                                                                        // Wenn gefunden wird alte Priority auf newPriority gechanged.
            Console.WriteLine($"Priority der Aufgabe '{aufgabenBeschreibung}' geändert zu: {newPriority}");                     // Ausgegeben wird message in Console to show that it was changed
            SaveAufgabeAlsDatei();                                                                                              // Saved es in text.txt
        }
        else                                                                                                                    // Falls not found, wird Message in Console ausgegeben. (Wrong User Input)                 
        {
            Console.WriteLine($"Aufgabe '{aufgabenBeschreibung}' nicht gefunden.");                                             
        }
    }

    public void ChangeAufgabenName(string alteBeschreibung, string neueBeschreibung)                                            // Methode zum Changen des AufgabenNames
    {
        AufgabenItem task = Aufgaben.Find(t => t.Beschreibung.Equals(alteBeschreibung, StringComparison.OrdinalIgnoreCase));    // Aufgabe mit angegebener Beschreibung wird gesucht, nicht case-sensitive weil StringComparison.OrdinalIgnoreCase used wird    

        if (task != null)                                                                                                       // Wird gechecked ob Aufgabe gefunden wird
        {
            task.Beschreibung = neueBeschreibung;                                                                               // Wenn gefunden wird alteBeschreibung auf neueBeschreibung gechanged
            Console.WriteLine($"Beschreibung von Aufgabe '{alteBeschreibung}' wurde zu '{neueBeschreibung}' gechanged.");       // Ausgegeben wird message in Console to show that it was changed
            SaveAufgabeAlsDatei();
        }
        else                                                                                                                    // Falls nicht gefunden, wird Message in Console ausgegeben (Wrong User Input)          
        {
            Console.WriteLine($"Aufgabe mit Beschreibung '{alteBeschreibung}' nicht gefunden.");
        }
    }


    public void DeleteAlleAufgaben()                                                                                            // Methode "DeleteAlleAufgaben" zum deleten aller Aufgaben (xd)
    {
        Aufgaben.Clear();                                                                                                       // Cleared entire Liste (also die text.txt)
        Console.WriteLine("Alle Aufgaben wurden gecleared.");                                                                   // Gibt dann message in die Console.
        SaveAufgabeAlsDatei();                                                                                                  // Saved es in text.txt (Datei ist jetzt empty)
    }

    private void SaveAufgabeAlsDatei()                                                                                          // Methode zum Saven der File - Damit es wieder aufgerufen werden kann 
    {
        using (StreamWriter writer = new StreamWriter(DateiPathAufgabe))                                                        // StreamWriter-Instance -> schreibt Text, "using" stellt sicher das der StreamWriter freigegeben werden kann
        {
            foreach (var task in Aufgaben)                                                                                      // Foreach Schleife/loop um durch jede Aufgabe in "Aufgaben-Liste" zu gehen und zu schreiben
            {
                writer.WriteLine($"{task.ID},{task.Beschreibung},{task.IstDone},{task.Priority}, {task.StartZeit},{task.EndZeit}"); // Für jede Aufgabe wird eine Zeile in die Datei geschrieben, enthält alle Infos wie angegeben. (CSV Format wäre prefered tbf my bad)

            }
        }
    }

    private void LoadAufgabeVonDatei()                                                                                          // Methode zum Laden der File - Damit das gespeicherte überhaupt aufgerufen wird
    {
        if (File.Exists(DateiPathAufgabe))                                                                                      // Überprüft ob die File existiert
        {
            Aufgaben.Clear();                                                                                                   // Löscht einmal alle Aufgaben der Liste to make sure, dass die Liste nur die geladenen
                                                                                                                                // Aufgaben der Datei included.
            foreach (string line in File.ReadLines(DateiPathAufgabe))
            {
                string[] parts = line.Split(',');                                                                               // Das teilt jede Zeile in Teile auf, trennt sie durch Kommas -> Es entsteht ein Array von Strings

                
                if (parts.Length == 6)                                                                                          // Überprüft ob die Anzahl der Teile korrekt ist, to make sure dass alles gültig ist.
                {
                    int id = int.Parse(parts[0]);                                                                               // Parst Aufgaben-ID in einen Integer
                    string beschreibung = parts[1];                                                                             // Nimmt die Beschreibung aus dem zweiten Teil
                    bool istDone = bool.Parse(parts[2]);                                                                        // Parst den Status in einen boolean-wert/value
                    AufgabenPrio priority;

                    if (Enum.TryParse(parts[3], out priority))                                                                  // Parst den Priority Teil in einen Enum-Wert vom Typ "AufgabenPrio"
                    {
                        DateTime startTime = DateTime.Parse(parts[4]);                                                          // Parst die Startzeit und Endzeit in "DateTime-Objekte"
                        DateTime endTime = DateTime.Parse(parts[5]);
                        Aufgaben.Add(new AufgabenItem(id, beschreibung, priority, startTime, endTime) { IstDone = istDone });   // Erstellt new "AufgabenItem-Object" mit geparsten Informationen und added es zu der "Aufgaben-Liste"
                    }                                                                                                           // Basically lädt die Method alle saved Daten/Informationen und updated die Aufgaben-Liste
                }
            }
        }
    }
}
           
