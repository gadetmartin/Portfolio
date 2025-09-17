CREATE TABLE "VILLE" (
	"id"	INTEGER,
	"Code_commune_INSEE"	INTEGER,
	"Nom_commune"	TEXT,
	"Code_postal"	INTEGER,
	"Info_Bonus"	TEXT,
	"Libellé_d_acheminement"	TEXT,
	PRIMARY KEY("id")
);

CREATE TABLE "CLUB_BASKET" (
	"numclub"	INTEGER,
	"nomclub"	VARCHAR NOT NULL UNIQUE,
	"villeclub"	INTEGER NOT NULL,
	PRIMARY KEY("numclub"),
	FOREIGN KEY("villeclub") REFERENCES "VILLE"("id")
);

CREATE TABLE "GYMNASE" (
	"numgym"	INTEGER,
	"nomgym"	VARCHAR NOT NULL UNIQUE,
	"adressegym"	VARCHAR,
	"villegym"	INTEGER NOT NULL,
	FOREIGN KEY("villegym") REFERENCES "VILLE"("id"),
	PRIMARY KEY("numgym")
);

CREATE TABLE "LISTE_GYM_CLUB" (
	"numclub"	INTEGER,
	"numgym"	INTEGER,
	FOREIGN KEY("numclub") REFERENCES "CLUB_BASKET"("numclub"),
	FOREIGN KEY("numgym") REFERENCES "GYMNASE"("numgym"),
	PRIMARY KEY("numclub","numgym")
);

CREATE TABLE "CATEGORIE" (
	"numcat"	INTEGER,
	"nomcat"	VARCHAR,
	"libellecat"	VARCHAR NOT NULL,
	"agemin"	INTEGER NOT NULL,
	"agemax"	INTEGER NOT NULL,
	PRIMARY KEY("numcat")
);

CREATE TABLE "CHAMPIONNAT" (
	"idChamp"	INTEGER,
	"secteurChamp"	VARCHAR(1) NOT NULL,
	"sexeChamp"	VARCHAR(1) NOT NULL,
	"categorie"	INTEGER NOT NULL,
	"numChamp"	INTEGER,
	"pouleChamp"	VARCHAR(1),
	"libelle_secteur"	VARCHAR,
	FOREIGN KEY("categorie") REFERENCES "CATEGORIE"("numcat"),
	PRIMARY KEY("idChamp")
);

CREATE TABLE "EQUIPE" (
	"id"	INTEGER,
	"numequipe"	INTEGER NOT NULL,
	"categorie"	INTEGER NOT NULL,
	"nomequipe"	VARCHAR,
	"coach"	INTEGER,
	"club"	INTEGER,
	PRIMARY KEY("id"),
	FOREIGN KEY("categorie") REFERENCES "CATEGORIE"("numcat"),
	FOREIGN KEY("club") REFERENCES "CLUB_BASKET"("numclub")
);

CREATE TABLE "ADHERENT" (
	"numLicenseAdh"	INTEGER,
	"nomAdh"	VARCHAR NOT NULL,
	"prenomAdh"	VARCHAR NOT NULL,
	"dateNaissanceAdh"	VARCHAR NOT NULL,
	"sexeAdh"	VARCHAR NOT NULL,
	"adresseAdh"	VARCHAR NOT NULL,
	"telAdh"	INTEGER,
	"mailAdh"	VARCHAR,
	"VilleAdh"	INTEGER NOT NULL,
	"ClubAdh"	INTEGER NOT NULL,
	"categorie"	INTEGER NOT NULL,
	"coach"	INTEGER,
	"equipe"	INTEGER,
	FOREIGN KEY("VilleAdh") REFERENCES "VILLE"("id"),
	FOREIGN KEY("ClubAdh") REFERENCES "CLUB_BASKET"("numclub"),
	FOREIGN KEY("categorie") REFERENCES "CATEGORIE"("numcat"),
	PRIMARY KEY("numLicenseAdh")
);

CREATE TABLE "MATCH" (
	"nummatch"	INTEGER,
	"datematch"	DATE NOT NULL,
	"heurematch"	TIME,
	"gymmatch"	INTEGER NOT NULL,
	"equipedomicile"	INTEGER,
	"equipevisiteur"	INTEGER,
	"championnat"	INTEGER NOT NULL,
	FOREIGN KEY("gymmatch") REFERENCES "GYMNASE"("numgym"),
	FOREIGN KEY("equipedomicile") REFERENCES "EQUIPE"("id"),
	FOREIGN KEY("equipevisiteur") REFERENCES "EQUIPE"("id"),
	FOREIGN KEY("championnat") REFERENCES "CHAMPIONNAT"("idChamp"),
	PRIMARY KEY("nummatch")
);
CREATE TABLE "INSCRIPTION_CHAMP" (
	"idInscrip"	INTEGER,
	"numequipe"	INTEGER,
	"championnat"	INTEGER,
	FOREIGN KEY("championnat") REFERENCES "CHAMPIONNAT"("idChamp"),
	FOREIGN KEY("numequipe") REFERENCES "EQUIPE"("id"),
	PRIMARY KEY("idInscrip")
);
