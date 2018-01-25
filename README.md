<b>Lab 4 - Azure</b>
Laborationen ska företrädesvis utföras två och två.
Databas
Appen ska vara en console-applikation, som kan ansluta till en Cosmos databas i Azure-molnet. Man ska kunna lägga till användare som har e-postadress och profilbild. Men för att hindra att vilka bilder som helst läggs upp så ska de granskas av en administratör innan de godkänns. Det ska också gå att se vilka användare som finns inlagda och vilka bilder som finns i kö för att godkännas.

<b>Skapa tabeller/collections för:</b>
användare (e-postadress)
bilder som inte granskats
bilder som granskats och är godkända
API
Skapa en Azure Function med en HTTP trigger. När man kör funktionen så kan den anropas genom att man skriver in en speciell URL i webbläsarens adressfält. Lokalt: localhost:portnr/sökväg?key=value . Det som står efter frågetecknet kallas querystring. Vi kan skicka "parametrar" till funktionen genom att lägga till dem till querystring. Om jag till exempel vill skicka en parameter bad med värdet "boll" så skriver man ?bad=boll sist i URL:en. Lägg till flera parametrar till querystring genom att sätta ett &-tecken emellan.

<b>Man ska kunna anropa funktionen på olika sätt:</b>
mode=viewReviewQueue - ska returnera en sträng som talar om vilka bilder som finns att granska
mode=approve&id=[id] - ska godkänna bilden med [id], dvs flytta över dokumentet från gransknings-collection till godkända-bilder-collection; sedan returnera en sträng som talar om ifall den lyckades eller inte
mode=reject&id=[id] - ska ta bort bilden med [id] från gransknings-collection och returnera en sträng som talar om ifall den lyckades eller inte

<b>Granskning</b>
Ni ska granska en annan grupps laboration och skriva en granskningsrapport. Rapporten ska innehålla minst två punkter med positiv feedback och en med konstruktiv kritik. (Skicka gärna den till gruppen ni granskar innan inlämning, så de hinner förbättra sig.) Om ni har gjort något annorlunda än gruppen så ska ni motivera varför ni valde att göra som ni gjorde och om ni skulle gjort något annorlunda med den kunskap ni har nu. Rapporten ska vara mellan 100-200 ord. (Det här stycket är 84 ord.)

<b>Inlämning och redovisning</b>
Laborationen redovisas under lektionstid för läraren. Ni ska demonstrera:
att man kan lägga till nya användare i console-delen
att man kan se alla användare i console-delen, samt alla bilder som står på kö för att godkännas
att API:et kan visa vilka bilder som står i kö för att godkännas
att API:et kan godkänna och ta bort bilder

Granskningsprotokollet skickas via slack i ett gruppmeddelande mellan båda laboranterna och läraren. Skicka som vanligt meddelande, i textformat eller PDF.

<b>Bedömningskriterier</b>
<b>För godkänt krävs</b>
båda apparna kan köras lokalt på din dator
console-appen kan lägga till nya användare och visa vilka bilder som står i kö
API:et ska kunna anropas med mode=viewReviewQueue
ni har lämnat in ett granskningsprotokoll
godkänd redovisning för läraren

<b>För väl godkänt krävs</b>
båda apparna kan köras i molnet
det ska finnas en Timer trigger function, som automatiskt godkänner alla bilder vars länk slutar med ".png", var 20:e sekund
API:et ska kunna anropas med viewReviewQueue, approve och reject
