# Travel Nest

Travel Nest è una piattaforma web dedicata alla prenotazione di alloggi per brevi periodi. Con Travel Nest, gli utenti possono trovare, prenotare e gestire facilmente alloggi disponibili per affitti temporanei.

## Funzionalità Principali

- **Registrazione e Autenticazione Utenti**: Gli utenti possono registrarsi, accedere e gestire i propri profili personali.
  
- **Prenotazione Online**: Prenota gli alloggi desiderati tramite un sistema di prenotazione intuitivo.

- **Gestione degli Alloggi**: I proprietari possono inserire dettagli sugli alloggi disponibili, gestire la disponibilità e gestire le prenotazioni ricevute.

- **Recensioni e Valutazioni**: Gli utenti possono lasciare recensioni e valutazioni sugli alloggi e sugli host.

- **Gestione del Profilo**: Aggiorna le informazioni di contatto, visualizza lo storico delle prenotazioni e gestisci le preferenze dell'account.

## Tecnologie Utilizzate

- **Framework**: ASP.NET Framework
- **Linguaggio di Programmazione**: C#
- **Database**: SQL Server

## Installazione

1. Clona il repository:

   ```bash
   git clone https://github.com/tuonome/travel-nest.git
   ```

2. Apri il progetto in Visual Studio e esegui il build della soluzione.

3. Assicurati di avere i seguenti pacchetti NuGet installati:

   - `Microsoft.AspNet.Mvc`
   - `EntityFramework`

4. Assicurati di avere un server SQL Server configurato e modifica la stringa di connessione nel file `web.config`.

5. Esegui gli script per creare le tabelle nel database `Travel_Nest`. Puoi farlo eseguendo gli script seguenti nel tuo server SQL Server:

   - **Admin**:

     ```sql
     CREATE TABLE [dbo].[Admin](
        [IDAdmin] [int] IDENTITY(1,1) NOT NULL,
          NOT NULL,
          NOT NULL,
          NOT NULL,
     PRIMARY KEY CLUSTERED 
     (
        [IDAdmin] ASC
     )ON [PRIMARY]
     ) ON [PRIMARY]
     GO
     ALTER TABLE [dbo].[Admin] ADD  DEFAULT ('Admin') FOR [Ruolo]
     GO
     ```

   - **Utenti**:

     ```sql
     CREATE TABLE [dbo].[Utenti](
        [IDUtente] [int] IDENTITY(1,1) NOT NULL,
          NULL,
          NULL,
          NULL,
          NULL,
          NOT NULL,
     PRIMARY KEY CLUSTERED 
     (
        [IDUtente] ASC
     )ON [PRIMARY]
     ) ON [PRIMARY]
     GO
     ALTER TABLE [dbo].[Utenti] ADD  DEFAULT ('User') FOR [Ruolo]
     GO
     ```

   - **Alloggi**:

     ```sql
     CREATE TABLE [dbo].[Alloggi](
        [IDAlloggio] [int] IDENTITY(1,1) NOT NULL,
        [IDUtente] [int] NULL,
          NULL,
        [Descrizione] [nvarchar](max) NULL,
          NULL,
        [PrezzoPerNotte] [decimal](10, 2) NULL,
        [Disponibilita] [bit] NULL,
     PRIMARY KEY CLUSTERED 
     (
        [IDAlloggio] ASC
     )ON [PRIMARY]
     ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
     GO
     ALTER TABLE [dbo].[Alloggi]  WITH CHECK ADD FOREIGN KEY([IDUtente])
     REFERENCES [dbo].[Utenti] ([IDUtente])
     GO
     ```

   - **ImmaginiAlloggi**:

     ```sql
     CREATE TABLE [dbo].[ImmaginiAlloggi](
        [IDImmagine] [int] IDENTITY(1,1) NOT NULL,
        [IDAlloggio] [int] NULL,
          NULL,
        [FileData] [image] NULL,
     PRIMARY KEY CLUSTERED 
     (
        [IDImmagine] ASC
     )ON [PRIMARY]
     ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
     GO
     ALTER TABLE [dbo].[ImmaginiAlloggi]  WITH CHECK ADD FOREIGN KEY([IDAlloggio])
     REFERENCES [dbo].[Alloggi] ([IDAlloggio])
     GO
     ```

   - **Prenotazioni**:

     ```sql
     CREATE TABLE [dbo].[Prenotazioni](
        [IDPrenotazione] [int] IDENTITY(1,1) NOT NULL,
        [IDUtente] [int] NULL,
        [IDAlloggio] [int] NULL,
        [DataCheckIn] [date] NULL,
        [DataCheckOut] [date] NULL,
        [StatoPrenotazione] [nvarchar](20) NULL,
     PRIMARY KEY CLUSTERED 
     (
        [IDPrenotazione] ASC
     )ON [PRIMARY]
     ) ON [PRIMARY]
     GO
     ALTER TABLE [dbo].[Prenotazioni]  WITH CHECK ADD FOREIGN KEY([IDAlloggio])
     REFERENCES [dbo].[Alloggi] ([IDAlloggio])
     GO
     ALTER TABLE [dbo].[Prenotazioni]  WITH CHECK ADD FOREIGN KEY([IDUtente])
     REFERENCES [dbo].[Utenti] ([IDUtente])
     GO
     ```

   - **Recensioni**:

     ```sql
     CREATE TABLE [dbo].[Recensioni](
        [IDRecensione] [int] IDENTITY(1,1) NOT NULL,
        [IDUtente] [int] NULL,
        [IDAlloggio] [int] NULL,
        [TestoRecensione] [nvarchar](max) NULL,
        [Valutazione] [int] NULL,
        [DataRecensione] [date] NULL,
     PRIMARY KEY CLUSTERED 
     (
        [IDRecensione] ASC
     )ON [PRIMARY]
     ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
     GO
     ALTER TABLE [dbo].[Recensioni]  WITH CHECK ADD FOREIGN KEY([IDAlloggio])
     REFERENCES [dbo].[Alloggi] ([IDAlloggio])
     GO
     ALTER TABLE [dbo].[Recensioni]  WITH CHECK ADD FOREIGN KEY([IDUtente])
     REFERENCES [dbo].[Utenti] ([IDUtente])
     GO
     ```

6. Avvia l'applicazione.

