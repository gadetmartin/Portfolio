--requete BD basket

--1) Donner la liste nominative des adhérents du club n°5 trié par ordre alphabétique
SELECT nomAdh
FROM ADHERENT
WHERE clubAdh = 5
ORDER BY nomAdh;

--2) Donner les nom des équipes qui affronteront l'équipe n°4 lors d'un match a domicile
SELECT nomEquipe
FROM EQUIPE E
INNER JOIN MATCH M ON M.equipevisiteur = E.numEquipe
WHERE M.equipeVisiteur = 4;

--3) Donner la liste nominative des joueuses qui sont né en 1995, qui participeront au match n°3
SELECT nomAdh
FROM ADHERENT A
INNER JOIN EQUIPE E ON E.numEquipe = A.equipe
INNER JOIN MATCH M ON M.equipevisiteur or M.equipedomicile = E.numEquipe
WHERE SUBSTR(dateNaissanceAdh,7)='1995' 
AND A.equipe IS NOT NULL
AND sexeAdh = 'Female'
AND numMatch = 3;

--4) Donner la liste des ville situé en Essone trié par ordre alphabétique
SELECT Nom_commune, Code_postal
FROM VILLE
WHERE SUBSTR(Code_postal,0,3) = "91"
ORDER BY Nom_commune;

--5) Donner la liste des match (désigné par les 2 equipes qui s'affrontent) qui se déroule dans le gymnase Georges Castaing
SELECT equipedomicile, equipevisiteur
FROM MATCH M
INNER JOIN GYMNASE G ON M.gymmatch = G.numGym
WHERE nomGym = 'Georges Castaing';

--6) Donner la liste des équipe coaché par le coach 10
SELECT numEquipe, nomequipe
FROM EQUIPE E
INNER JOIN ADHERENT A ON A.equipe = E.numequipe
GROUP BY numequipe, nomequipe
HAVING E.coach = 10;

--7) Donner la liste des joueur né en 1998 et appartenant a la categorie U20+
SELECT nomAdh
FROM ADHERENT A
INNER JOIN CATEGORIE C ON A.categorie = C.numCat
WHERE equipe IS NOT NULL
AND SUBSTR(dateNaissanceAdh,7)='1998'
AND nomcat = 'U20+';

--8) Donner la liste nominative des gymnases situés a Palaiseau
SELECT nomGym
FROM GYMNASE G
INNER JOIN VILLE V ON V.id = G.villeGym
WHERE Nom_commune = 'PALAISEAU';

--9) Donner la liste des match (désigné par les 2 equipes qui s'affrontent) et le nom gymnase qui acceuille le matche se déroulant le 2 janvier 2021
SELECT equipeDomicile, equipeVisiteur, nomGym
FROM MATCH M
INNER JOIN GYMNASE G ON M.gymmatch = G.numGym
WHERE dateMatch = "2021-01-02";

--10) Donner la liste nominative des catégories dont l'age minimum est 18 ans et qui peuvent partciper a un tournoi
SELECT DISTINCT nomCat
FROM CATEGORIE C
INNER JOIN CHAMPIONNAT CHAMP ON C.numCat = CHAMP.categorie
WHERE ageMin = 20
AND CHAMP.categorie IS NOT NULL;

--11) Donner le nombre de championnat en 2021
SELECT COUNT(numMatch)as nombre_match
FROM MATCH
WHERE SUBSTR(dateMatch,0,5) = "2021";

--12) Donner le nombre d'adhérent du club n°4
SELECT COUNT(numLicenseAdh)as nombre_adh
FROM ADHERENT
WHERE ClubAdh = 4;

--13) Donner le nombre d'equipe de la catégorie U20+
SELECT COUNT(numEquipe)as nom_Eq
FROM EQUIPE E
INNER JOIN CATEGORIE C ON C.numCat = E.categorie
WHERE nomCat = 'U20+';

--14) Donner la liste des coach
SELECT prenomAdh, nomAdh
FROM ADHERENT 
WHERE coach IS NOT NULL;

--15) Donner la liste nominative des coach ainsi que le club dans lequel ils sont
SELECT nomAdh, nomClub
FROM ADHERENT A
INNER JOIN CLUB_BASKET C ON C.numClub = A.ClubAdh
WHERE coach IS NOT NULL;

--16) Donner le nombre de personne dans chaque equipe
SELECT equipe, COUNT(*) as nb_personne
FROM adherent
WHERE equipe IS NOT NULL
GROUP BY equipe;

--17) Donner le nombre de joueurs homme
SELECT COUNT(numLicenseAdh)
FROM ADHERENT 
WHERE sexeAdh = 'Male';

--18) Donner le nombre de joueuses
SELECT COUNT(numLicenseAdh)
FROM ADHERENT 
WHERE sexeAdh = 'Female';

--19) Donner le nombre d'equipe inscrite au championnat n°17
SELECT COUNT(numequipe)
FROM INSCRIPTION_CHAMP
WHERE championnat = 17;

--20) Donner les nom des gymanses utilisé par le club USP
SELECT nomGym
FROM GYMNASE G 
INNER JOIN LISTE_GYM_CLUB LGC ON LGC.numGym = G.numGym
INNER JOIN CLUB_BASKET C ON C.numclub = LGC.numClub
WHERE nomClub = 'USP';