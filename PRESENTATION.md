# RoomBook – Präsentation

**Modul 322 | Nelo Nissle | August 2026**

---

## Folie 1 – Ausgangslage & Personas

**Problem:** Neumann Consulting AG hat 6 Sitzungszimmer, verwaltet per Outlook-Kalender → Doppelbuchungen, fehlende Übersicht.

**Lösung:** RoomBook – Blazor Web App für Suche, Reservation und Stornierung.

**Personas:**
- 👩 **Léa** (24, neu, wenig IT-Erfahrung) → braucht selbsterklärende UI, klare Fehlermeldungen
- 👨 **Tobias** (38, Rot-Grün-Sehschwäche) → Status darf NICHT nur durch Farbe erkennbar sein

---

## Folie 2 – Entwurfsentscheide

**Technologiewahl:** Blazor Web App (Server-Side) wegen:
- Browser-Nutzung auf PC & Tablet
- C# durchgehend, kein JavaScript-Aufwand
- EditForm + DataAnnotationsValidator nativ vorhanden

**Wichtigste Entwurfsentscheide:**
- Bootstrap 5 Navbar → konsistente Navigation auf allen Screens
- Karten-Layout auf Startseite → Gestaltgesetz Nähe (klare Einstiegspunkte)
- Filter oben in Raumliste → Primat der Reihenfolge
- Status immer Icon + Text + Farbe → Barrierefreiheit für Tobias

---

## Folie 3 – Live-Demo (5 Screens)

1. **Startseite** → 3 Karten als Einstiegspunkte
2. **Raumsuche** → Filter (Kapazität, Ausstattung), Statussymbole
3. **Raumdetail** → Ausstattungstabelle, heutige Reservationen
4. **Reservation erfassen** → Formular mit Validierung, Pflichtfelder mit `*`
5. **Stornierung** → ID eingeben ODER direkt in Tabelle stornieren

**Grenzfall-Demo:** Doppelbuchungsversuch für Raum Beta, 09:00–10:00 →  
→ Fehlermeldung: *„Der Raum ist in diesem Zeitfenster bereits belegt."*

---

## Folie 4 – Usability-Test

**Walkthrough-Ergebnis:**
- Léa-Szenario: 8 Schritte, 2 Probleme gefunden
- P1: Fehlermeldung war anfangs unter dem Scroll-Bereich
- P2: Dropdown ohne Erklärung

**SUS-Score: 77.5 / 100** (Benchmark «gut»)

**Umgesetzte Optimierungen:**
1. ✅ Fehlermeldung an den Anfang des Formulars verschoben
2. ✅ Hilfetexte mit `aria-describedby` zu allen Formularfeldern hinzugefügt

---

## Folie 5 – Barrierefreiheit

| Massnahme | Code-Umsetzung |
|---|---|
| Status nicht nur Farbe | `🟢 Frei` / `🔴 Belegt` + `aria-label="Status: Frei"` |
| Screenreader-Labels | `<label for="">`, `aria-label`, `aria-required` |
| Dynamische Meldungen | `role="alert"`, `aria-live="polite/assertive"` |
| Fokus-Reihenfolge | Natürliche DOM-Reihenfolge = visuelle Reihenfolge |
| Farbkontrast | Bootstrap 5 Standard → WCAG AA erfüllt |

**WCAG 2.1 Checkliste:** 9/9 Kriterien erfüllt ✅

---

## Folie 6 – Reflexion

**Grösste Herausforderung:**  
Zeitfenster-Kollisionsprüfung (Grenzfälle: direkt aufeinanderfolgende Reservationen)

**Wichtigste Erkenntnis:**  
Barrierefreiheit kostet wenig Aufwand, wenn sie von Anfang an mitgedacht wird.

**Wenn ich es nochmal täte:**  
Kalenderansicht (Wochenübersicht), automatisierter Axe-Test, Unit Tests für Service-Schicht.

---

## Lokaler Start

```bash
cd RoomBook
dotnet run
# → http://localhost:5000
```
