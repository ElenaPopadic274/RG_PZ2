# RAČUNARSKA GRAFIKA(SOFTVER SA KRITIČNIM ODZIVOM U ELEKTROENERGETSKIM SISTEMIMA) | COMPUTER GRAPHICS (CRITICAL RESPONSE SOFTWARE IN POWER SYSTEMS)

Cilj drugog predmetnog zadatka je postavljanje elemenata elektroenergetske mreže na 3D mapu. Mapu je potrebno postaviti 
kao 2D sliku - ploču na dnu scene. Entiteti će se postavljati na odgovarajuće koordinate na mapi, na osnovu podataka iz 
Geographic.xml fajla. Donji levi ugao mape ima koordinate lat: 45,2325, lon: 19.793909, a gornji desni lat: 45,277031, 
lon: 19.894459. Sve entitete i vodove van ove površine ignorisati. Potrebno je naspram širine i visine ploče sa slikom 
mape izračunati koliki je relativni pomeraj jednog stepena u 3D sceni, i na osnovu toga postaviti entitete i vodove na 
svoje pozicije. 

Entiteti se iscrtavaju kao kocke, tako da oblik kocke nema nikakvih „rupa“ odnosno delova modela koji se ne prikazuju.
Kod entiteta tipa Substation/Node/Switch koji se preklapaju, crtati ih jednog iznad drugog. 

Potrebno je da se uz pritisak na levi taster miša, mapa može pomerati (pan-ovati) i zumirati pomoću scroll točkića. 
Nezavisno od nivoa zumiranja, objekti treba uvek da stoje na svojim koordinatama. Treba omogućiti da se scena slobodno 
rotira oko svog centra prilikom pomeranja miša dok je pritisnut scroll točkić. 

Entiteti dodati na mapu trebaju da imaju obezbedjen hit testing tako da se mogu ispisati informacije o njima (id, ime, tip)
u vidu tooltipa ili na sličan način, u blizini kursora. Takođe hit testing treba da funkcioniše i na vodovima, tako što će
se promeniti boja entiteta koje vod spaja. 

Vodove obavezno crtati pomoću trouglova tako da izgledaju kao „cevi“ čiji je poprečni presek u obliku trougla ili kvadrata 
i potrebno je iscrtavati ih na osnovu property-ja Vertices iz fajla Geographic.xml. Na ovaj način, svi vodovi neće završiti
ili započeti iscrtavanje od početnog/krajnjeg elementa kojeg spajaju, pa je potrebno kompletirati iscrtavanje svih vodova,
tako da počnu i završe se na čvorovima koji stoje na krajevima voda. Vodove crtati različitim bojama, na osnovu tipa 
materijala od kojeg je vod konstruisan. 

Pošto se elementi mreže iscrtavaju jedni iznad drugih u slučaju preklapanja, vod koji treba da završi u tako nekom elementu
će ići do elementa koji je nacrtan „na dnu“, a prilikom hit testing-a, označavaće se element koji je zapravo spojen. 

Kao dodatne opcije u okviru interfejsa aplikacije, potredno je:  </br>
-Omogućiti sakrivanje/prikazivanje neaktivnog dela mreže: sakrivaju se vodovi koji izlaze iz prekidača čiji je status 
„open“, kao i entiteti koji su za taj vod SecondEnd. </br>
-Omogućiti promenu boje entiteta tipa Switch na crvenu ukoliko im je status “closed” i na zelenu ukoliko im je status 
“open”, ali i da se boja može vratiti na inicijalnu. </br>
-Omogućiti promenu boje vodova na osnovu otpornosti: ispod 1 - crvena boja; od 1 do 2 - narandžasta; iznad 2 - žuta boja, 
ali i da se boja može vratiti na inicijalnu. </br>
-Omogućiti prikazivanje/sakrivanje svih objekata (osim linija) na osnovu broja konekcija: prva opcija – od 0 do 3; 
druga – od 3 do 5; treća – vise od 5 konekcija. 

 --------------------------------------------------------------------------------------------------------------------------------------------------------------------
 
 # COMPUTER GRAPHICS (CRITICAL RESPONSE SOFTWARE IN POWER SYSTEMS) | COMPUTER GRAPHICS (CRITICAL RESPONSE SOFTWARE IN POWER SYSTEMS) 

The goal of the second subject task is to place the elements of the electric power network on a 3D map. The map needs 
to be set up as a 2D image - a board at the bottom of the scene. The entities will be placed at the appropriate coordinates 
on the map, based on data from the Geographic.xml file. The lower left corner of the map has coordinates lat: 45.2325, 
lon: 19.793909, and the upper right corner lat: 45.277031, lon: 19.894459. Ignore all entities and lines outside this area. 
It is necessary to calculate the relative displacement of one degree in the 3D scene in relation to the width and height of 
the map image board, and based on that, place the entities and lines in their positions. 

Entities are drawn as cubes, so that the shape of the cube does not have any "holes" or parts of the model that are not displayed. 
For overlapping Substation / Node / Switch entities, draw them one above the other.

It is necessary that by pressing the left mouse button, the map can be moved (panned) and zoomed using the scroll wheel. 
Regardless of the zoom levels, objects should always be in their coordinates. You should allow the scene to rotate freely around 
its center when you move the mouse while the scroll wheel is pressed.

Entities added to the map should have hit testing provided so that information about them (id, name, type) can be printed as a 
tooltip or similar, near the cursor. Hit testing should also work on lines, by changing the color of the entities that connect the lines. 

Be sure to draw the lines with triangles so that they look like "pipes" whose cross section is in the shape of a triangle or square 
and you need to draw them based on the Vertices property from the Geographic.xml file. In this way, not all lines will end or start 
drawing from the start / end element they connect, so it is necessary to complete the drawing of all lines so that they start and end 
at the nodes that stand at the ends of the lines. Draw lines in different colors, based on the type of material from which the line 
is constructed. Since the elements of the grid are drawn one above the other in the case of overlap, the line that should end in such an 
element will go to the element that is drawn "at the bottom", and during hit testing, the element that is actually connected will be marked. 

As additional options within the application interface, it is important to:  </br>
-Enable hiding / showing the inactive part of the network: hiding lines coming out of switches whose status is "open", as well as entities 
that are for that platoon SecondEnd. </br>
-Allow the color of the Switch type entity to change to red if their status is "closed" and to green if their status is "open", but also 
that the color can be returned to the initial. </br>
-Enable the color change of the conductors based on the resistance: below 1 - red color; from 1 to 2 - orange; above 2 - yellow color, 
but also that the color can return to the initial. </br>
-Enable to show / hide all objects (except lines) based on the number of connections: first option - from 0 to 3; the second - from 3 to 5; 
third - more than 5 connections.
