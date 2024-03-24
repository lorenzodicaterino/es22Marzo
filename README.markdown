# ESERCITAZIONE 22 MARZO 2024

```sql
--drop table Evento
CREATE TABLE Evento(
eventoID INT PRIMARY KEY IDENTITY(1,1),
nomeEvento VARCHAR(250) NOT NULL,
descrizioneEvento TEXT,
dataEvento DATETIME NOT NULL,	
luogoEvento VARCHAR(250) NOT NULL,
capacitaMassima INT NOT NULL CHECK (capacitaMassima>0),
deleted DATETIME
);

--drop table pARTECIPANTE
CREATE TABLE Partecipante (
partecipanteID INT PRIMARY KEY IDENTITY(1,1),
nominativo VARCHAR(250) NOT NULL,
telefono VARCHAR(30) NOT NULL,
email VARCHAR(250),
deleted DATETIME
);

--DROP TABLE Risorse
CREATE TABLE Risorse(
risorseID INT PRIMARY KEY IDENTITY(1,1),
quantita INT NOT NULL CHECK (quantita >= 0),
costo DECIMAL(5,2) NOT NULL CHECK (costo>=0),
fornitore VARCHAR(250) NOT NULL,
tipo VARCHAR(250) NOT NULL CHECK(tipo IN ('cibo', 'bevanda','attrezzatura')),
eventoRIF INT NULL,
FOREIGN KEY (eventoRIF) REFERENCES Evento(eventoID),
deleted DATETIME
);

--DROP TABLE Evento_Partecipante;
CREATE TABLE Evento_Partecipante(
eventoRIF INT NOT NULL,
partecipanteRIF INT NOT NULL,
FOREIGN KEY (eventoRIF) REFERENCES Evento(eventoID),
FOREIGN KEY (partecipanteRIF) REFERENCES Partecipante(partecipanteID),
PRIMARY KEY (eventoRIF, partecipanteRIF)
);
```

23/03/2024
Ho provato a svolgere tutti i puntie al momento gli unici che mi mancano da implementare sono il DELETE e l'importazione e l'esportazione dei file .CSV (oltre all'implementazione dell'UPDATE da terminare). 

24/03/2024
Sono riuscito a svolgere tutti i punti (apparte l'importazione tramite file .CSV) in maniera non esageratamente ottimizzata, però "if it works, don't touch it"