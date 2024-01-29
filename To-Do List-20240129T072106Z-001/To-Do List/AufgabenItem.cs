using System;

class AufgabenItem                                                                                                                  // Class für AufgabenItems, basically alles wichtige
{
    public int ID { get; set; }                                                                                                     // defined ID (WIP)
    public string Beschreibung { get; set; }                                                                                        // defined string Beschreibung und erlaubt get + set
    public bool IstDone { get; set; }                                                                                               // defined bool "IstDone" und erlaubt get + set
    public AufgabenPrio Priority { get; set; }                                                                                      // defined AufgabenPrio Priority und erlaubt get + set
    public DateTime? StartZeit {  get; set; }                                                                                       // defined DateTime StartZeit und erlaubt get + set
    public DateTime? EndZeit { get; set; }                                                                                          // defined DateTime EndZeit und erlaubt get + set

    public AufgabenItem(int id, string beschreibung, AufgabenPrio priority, DateTime? startZeit, DateTime? endZeit)                 // this makes it possible to use "AufgabenItem" mit allen Funktionen
    {
        ID = id;
        Beschreibung = beschreibung;
        Priority = priority;
        IstDone = false;
        StartZeit = startZeit;
        EndZeit = endZeit;
    }
}

enum AufgabenPrio                                                                                                                    // declared enum AufgabenPrio
{                                                                                                                                    // 3 Konstanten/Constants
    NotImportant,                                                                                                                    // enums machen es possible bennante Konstanten zu erstellen die festen
    Average,                                                                                                                         // Datentypen zugeordnet sind
    Wichtig                                                                                                                          // enums machen Code übersichtlicher und ordentlicher
}                                                                                                                                    // Constants/Prios = NotImportant, Average, Wichtig


