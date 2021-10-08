Hashing algorithm

VU block-chain pirma užduotis

* Paleidimas
    - ./Hash-algorithm -in | -inf -out | -outf <-t> <inputs | filenames>

    - -in - Skaitymas iš komandos eilutės
    - -inf - Skaitymas iš failų
    - -out - Rezultatų išvedimas į komandinę eilutę
    - -outf - Rezultatų išvedimas į failą
    - -t - neprivalomas parametras, atlieka visus testus (output length, collision, similarity, speed tests)

Funkcijos analizė:

- Funkcijos input gali būti bet kokio ilgio

- Funkcijos įšvestis visada bus 64 simbolių hex'as

- Funkcija yra deterministinė: tam pačiam input visada bus toks pat output

- Funkcija veikia greitai: konstitucija.txt failo hash'inimas užtruko 8ms

- Funkcija atspari kolizijai: buvo atlikta 100000 testų su skirtingais input string'ais ir nebuvo aptikta jokių kolizijų

Hash'ų skirtingumo testų rezultatai: 

    - Mažiausias skirtumas HEX lygmenyje: 46.88%
    - Didžiausias skirtumas HEX lygmenyje: 96.88%
    - Mažiausias skirtumas HEX lygmenyje: 81.17%

    - Mažiausias skirtumas Binary lygmenyje: 12.89%
    - Didžiausias skirtumas Binary lygmenyje: 45.31%
    - Mažiausias skirtumas Binary lygmenyje: 29.73%