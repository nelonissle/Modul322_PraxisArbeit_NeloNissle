# RoomBook – Sitzungszimmer-Reservationssystem

**Modul 322 – Benutzerschnittstellen entwerfen und implementieren**  
**Schüler:** Nelo Nissle  
**Datum:** August 2026  
**Technologie:** Blazor Web App (.NET 10, Interaktiver Server-Render-Modus)

---

## Titelblatt

| Feld | Wert |
|---|---|
| Name | Nelo Nissle |
| Modul | 322 – Benutzerschnittstellen entwerfen und implementieren |
| Technologie | Blazor Web App (.NET 10, Interactive Server) |
| Datum | August 2026 |

---

## 1. Nutzungsanalyse (HZ1)

### 1.1 Nutzungsumfeld

Die Mitarbeitenden der Neumann Consulting AG arbeiten im Büro an Desktop-PCs und Tablets. Die Reservation von Sitzungszimmern erfolgt heute über einen gemeinsamen Outlook-Kalender, der zu Doppelbuchungen und fehlender Übersicht führt. Die neue Applikation «RoomBook» soll im Browser (intern) genutzt werden und muss auf PC und Tablet gleich gut bedienbar sein.

### 1.2 Personas

**Persona 1 – Léa Dupont (Neue Mitarbeiterin)**
- Alter: 24 Jahre
- Erfahrung: Wenig IT-Erfahrung, kommt aus dem kaufmännischen Bereich
- Ziel: Schnell einen Raum reservieren, ohne sich lange einarbeiten zu müssen
- Anforderung: Klare Navigation, selbsterklärende Formulare, verständliche Fehlermeldungen

**Persona 2 – Tobias Bauer (Rot-Grün-Sehschwäche)**
- Alter: 38 Jahre
- Erfahrung: IT-affin, nutzt täglich verschiedene Systeme
- Einschränkung: Kann rote und grüne Farbtöne nicht zuverlässig unterscheiden
- Anforderung: Status «frei/belegt» muss durch Icons und Text zusätzlich zur Farbe angezeigt werden

### 1.3 Nutzungsanforderungen (User Stories)

| Priorität | User Story |
|---|---|
| Hoch | Als Mitarbeiterin möchte ich auf der Startseite sofort sehen, wie ich einen Raum reservieren kann. |
| Hoch | Als Mitarbeiterin möchte ich Räume nach Kapazität und Ausstattung filtern, damit ich den passenden Raum finde. |
| Hoch | Als Mitarbeiter möchte ich den Belegungsstatus eines Raums anhand von Icon und Text erkennen (nicht nur Farbe). |
| Hoch | Als Mitarbeiterin möchte ich ein einfaches Formular ausfüllen, um einen Raum zu reservieren. |
| Hoch | Als Mitarbeiter möchte ich bei einer Doppelbuchung eine verständliche Fehlermeldung erhalten. |
| Mittel | Als Mitarbeiter möchte ich meine bestehende Reservation stornieren können. |
| Mittel | Als Mitarbeiterin möchte ich nach der Reservation eine Bestätigungsmeldung sehen. |
| Tief | Als Mitarbeiter möchte ich Bemerkungen zur Reservation hinzufügen können. |

---

## 2. Entwurf der Benutzerschnittstelle (HZ2)

### 2.1 Screen-Flow (Navigation/Abfolge)

```
Startseite (/)
    ├──> Raumsuche (/rooms)
    │       └──> Raumdetail (/rooms/{id})
    │               └──> Reservation erfassen (/reserve?roomId=X)
    ├──> Reservation erfassen (/reserve)
    └──> Stornierung (/cancel)
```

### 2.2 Wireframe-Beschreibung

**Screen 1 – Startseite (Übersicht)**
- Header mit Logo und Hauptnavigation (Bootstrap Navbar)
- Drei Karten: Raumsuche, Reservieren, Stornieren
- Jede Karte: Icon, Titel, Kurzbeschreibung, Link-Button
- Begründung: Klare Einstiegspunkte; Gestaltgesetz der Nähe (zusammengehörige Infos gruppiert)

**Screen 2 – Raumsuche/-liste**
- Filterbereich: Kapazität (Zahlfeld), Checkboxen für Ausstattung
- Raumkarten-Grid: Status-Badge (Icon + Text + Farbe), Kapazität, Ausstattungsliste, Link «Details»
- Begründung: Filterbereich oben = Primat der Reihenfolge; Karten statt Tabelle = bessere Scanbarkeit

**Screen 3 – Raumdetail**
- Titel mit Status-Badge
- Tabelle: Ausstattungsdetails
- Liste: Heutige Reservationen
- Schnelllink: «Diesen Raum reservieren»

**Screen 4 – Reservation erfassen**
- EditForm mit Validierung
- Pflichtfelder mit `*` gekennzeichnet und aria-required
- Fehlerbereich oben, Erfolgsbereich nach Absenden

**Screen 5 – Stornierung erfassen**
- Linke Seite: ID-Eingabe + Stornieren-Button
- Rechte Seite: Tabelle aller Reservationen mit Direkt-Stornieren-Button

### 2.3 Interaktionsprinzipien

- **Konsistente Navigation:** Bootstrap Navbar immer sichtbar
- **Feedback:** Alert-Komponenten mit `role="alert"` und `aria-live` für Screenreader
- **Pflichtfelder:** mit `*` und `aria-label="Pflichtfeld"` markiert
- **Statussymbole:** Immer Icon + Text + Farbe (nie nur Farbe)

---

## 3. Implementierung (HZ3)

### 3.1 Technologiewahl – Blazor Web App

**Gewählt:** Blazor Web App (.NET 10) mit Interactive Server Render Mode

**Begründung:**
- Primäre Nutzung im Browser auf Desktop und Tablet → Web-Technologie sinnvoll
- Server-Side Rendering ermöglicht Datenzugriff ohne API-Aufwand
- C# durchgehend (kein JavaScript erforderlich)
- EditForm + DataAnnotationsValidator für Formularvalidierung nativ vorhanden
- ARIA-Attribute für Barrierefreiheit direkt in Razor-Templates integrierbar

### 3.2 Projektstruktur

```
RoomBook/
├── Models/
│   ├── Room.cs              – Raumdatenmodell
│   └── Reservation.cs       – Reservationsmodell mit Data Annotations
├── Services/
│   └── RoomBookService.cs   – In-Memory-Datenhaltung, Business Logic
├── Components/
│   ├── Pages/
│   │   ├── Home.razor       – Startseite/Übersicht
│   │   ├── Rooms.razor      – Raumsuche/-liste mit Filter
│   │   ├── RoomDetail.razor – Raumdetail mit Belegungsplan
│   │   ├── Reserve.razor    – Reservation erfassen
│   │   └── Cancel.razor     – Stornierung erfassen
│   └── Layout/
│       ├── MainLayout.razor – Seitenlayout
│       └── NavMenu.razor    – Hauptnavigation
└── Program.cs               – Service-Registrierung (Singleton)
```

### 3.3 Kritische Komponente – Kollisionsprüfung (Doppelbuchung)

**Komponente:** `RoomBookService.IsRoomAvailable()` und `CreateReservation()`

**Logik:** Zwei Zeitfenster überlappen, wenn `start1 < end2 && end1 > start2`.

```csharp
return !_reservations.Any(r =>
    r.RoomId == roomId &&
    r.Date == date &&
    r.Id != excludeReservationId &&
    start < r.EndTime &&
    end > r.StartTime);
```

**Testergebnis:** Bei Doppelbuchungsversuch erscheint die Meldung:  
*„Der Raum ist in diesem Zeitfenster bereits belegt. Bitte wählen Sie ein anderes Zeitfenster."*

Grenzfälle getestet:
- Gleiches Zeitfenster → Doppelbuchung erkannt ✅
- Lückenlos aneinander (09:00–10:00 + 10:00–11:00) → kein Konflikt ✅
- Überlappend (09:00–10:30 + 10:00–11:00) → Doppelbuchung erkannt ✅

### 3.4 Screenshots

Screenshots der Applikation sind durch lokalen Start zugänglich (`dotnet run` im `RoomBook/`-Verzeichnis).

---

## 4. Usability-Überprüfung (HZ4)

### 4.1 Walkthrough-Protokoll

**Szenario:** Mitarbeiterin Léa möchte Raum Beta für Freitag 10:00–11:00 reservieren.

| Schritt | Beobachtung |
|---|---|
| 1. Startseite öffnen | Drei Karten sofort sichtbar, klarer Einstieg |
| 2. «Jetzt reservieren» klicken | Formular öffnet sich, Pflichtfelder mit * sichtbar |
| 3. Name eingeben | Kein Problem |
| 4. Raum aus Dropdown wählen | Raum Alpha/Beta etc. mit Kapazität sichtbar |
| 5. Datum eingeben | Native Date-Picker, intuitiv |
| 6. Zeit 09:00–10:00 wählen | Kein Problem |
| 7. «Reservieren» klicken | Doppelbuchungsfehler erscheint → Problem erkannt |
| 8. Zeit auf 10:00–11:00 anpassen | Reservation erfolgreich, Bestätigung sichtbar |

**Gefundene Probleme:**
- P1: Fehlermeldung bei Doppelbuchung war anfangs nicht sofort sichtbar (war unterhalb des Fold)
- P2: Dropdown zeigte beim Laden «-- Bitte Raum wählen --» ohne weitere Erklärung

### 4.2 SUS-Fragebogen (Peer-Test)

| Frage | Score (1-5) |
|---|---|
| Ich würde das System gerne häufig nutzen | 4 |
| Das System ist unnötig komplex | 2 |
| Das System ist einfach zu nutzen | 4 |
| Ich brauche die Hilfe einer technischen Person | 1 |
| Die Funktionen sind gut integriert | 4 |
| Zu viele Inkonsistenzen | 2 |
| Die meisten lernen es schnell | 5 |
| Das System ist umständlich | 2 |
| Ich fühle mich beim Nutzen sicher | 4 |
| Ich musste viel lernen | 1 |

**SUS-Score:** ((4-1)+(5-2)+(4-1)+(5-1)+(4-1)+(5-2)+(5-1)+(5-2)+(4-1)+(5-1)) × 2.5 = **77.5 / 100** (gut)

### 4.3 Umgesetzte Optimierungen

1. **Fehlermeldung nach oben verschoben:** `alert-danger` steht jetzt vor den Formularfeldern, sodass es beim Absenden sofort sichtbar ist.
2. **Hilfetexte zu Feldern hinzugefügt:** `form-text`-Elemente mit `aria-describedby` erklären das erwartete Format (z.B. Zeitformat HH:MM).

---

## 5. Barrierefreiheit (HZ5)

### 5.1 Umgesetzte Massnahmen

| Anforderung | Umsetzung |
|---|---|
| Status nicht nur durch Farbe | Badge mit Icon (🟢/🔴) + Text «Frei»/«Belegt» + `aria-label` |
| Labels für Screenreader | Alle Inputs mit `<label for="">`, ARIA-Attribute |
| Tab-/Fokus-Reihenfolge | HTML-Quellreihenfolge entspricht visueller Reihenfolge |
| Farbkontrast WCAG AA | Bootstrap 5 mit Standard-Farben erfüllt WCAG AA |
| `aria-live` für dynamische Inhalte | Erfolgs-/Fehlermeldungen mit `aria-live="polite/assertive"` |
| `role="alert"` | Alle Alert-Boxen mit `role="alert"` |
| `aria-required` | Pflichtfelder mit `aria-required="true"` |
| `aria-label` für Buttons | Alle Buttons mit beschreibendem `aria-label` |

### 5.2 Prüf-Checkliste (WCAG 2.1)

| Kriterium | Status | Bemerkung |
|---|---|---|
| 1.1.1 Nicht-Text-Inhalt (Alt-Text) | ✅ | Icons dekorativ mit `aria-hidden="true"` |
| 1.3.1 Info und Beziehungen | ✅ | Labels, Tabellen-Header mit `scope` |
| 1.4.1 Farbe nicht allein | ✅ | Status immer Icon + Text + Farbe |
| 1.4.3 Kontrast (AA) | ✅ | Bootstrap 5 Standardfarben |
| 2.1.1 Tastatur-Nutzung | ✅ | Alle Funktionen per Tab erreichbar |
| 2.4.3 Fokus-Reihenfolge | ✅ | Natürliche DOM-Reihenfolge |
| 3.3.1 Fehlererkennung | ✅ | Fehlermeldungen mit `role="alert"` |
| 3.3.2 Beschriftungen | ✅ | Alle Felder korrekt beschriftet |
| 4.1.2 Name, Rolle, Wert | ✅ | ARIA-Attribute durchgängig |

---

## 6. Reflexion

**Was war schwierig?**  
Die korrekte Implementierung der Zeitfenster-Kollisionsprüfung mit `TimeOnly` in C# erforderte genaues Nachdenken über Grenzfälle (z.B. direkt aufeinanderfolgende Reservationen). Auch die korrekte ARIA-Nutzung für dynamische Inhalte (`aria-live`, `role="alert"`) war anfangs nicht selbstverständlich.

**Was würde ich anders machen?**  
Bei mehr Zeit würde ich eine Kalenderansicht (Wochenübersicht) implementieren, die alle Räume und ihre Belegung auf einen Blick zeigt. Zudem würde ich Unit Tests für den `RoomBookService` schreiben und die Applikation mit einem automatisierten Accessibility-Tool (Lighthouse/axe) testen.

---

## Lokaler Start

```bash
cd RoomBook
dotnet run
```

Die Applikation ist dann unter http://localhost:5000 (oder dem angezeigten Port) erreichbar.
