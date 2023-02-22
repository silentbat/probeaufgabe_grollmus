Gebaut und getestet mit dotnet `7.0.200`.
Für eine dev env Version, einfach mit `dotnet watch` starten, es öffnet sich automatisch ein Browserfenster.

Diese Webanwendung ist stark angelehnt an das Intro Tutorial von dotnet für Blazor Pages, jeder der dieses kennt sollte sich direkt zurecht finden.

Das Backend ist in dieser Iteration eine relativ simple sqlite Datenbank, welche direkt aus der Blazor App befüllt und gelesen wird.

Es wurden interne IDs vergeben, um einzelne Geräte löschen zu können, auch wenn sie mehrfach hinzugefügt wurden und somit von ihren JSON Daten nicht unterscheidbar sind.

Es gibt auch einen auskommentierten "delete all data" Button, falls das für Testzwecke hilfreich ist, einfach wieder einkommentieren (`./Pages/Index.razor:line 50`).

An einigen Stellen sowie in [Ideas](ideas.md) wurde in Kommentaren festgehalten was an dieser Stelle für ein Projekt die möglichen nächsten Schritte wären, sofern aus diesem Prototypen etwas größeres werden soll.