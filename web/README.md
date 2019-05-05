#  Project-PixBlocks-Addition Web

### Podstawowe komendy do zarządzania częścią web'ową:

- Instalacja `angular@cli`:
```
npm install -g @angular/cli
```

- Instalacja pakietów z `package.json`:
```
cd web
npm install
```

- Uruchamianie serwera:
```
npm start
```
lub
```
ng serve --open
```
### Translacje i budowanie

- Budowanie wersji produkcyjnej, z wszystkimi wersjami językowymi:
```
npm run build-locale
```

- Uruchamianie konkretnej wersji językowej frontendu:(polska)
```
ng serve --configuration=pl
```

- Utworzenie pliku translacji(messages.xlf):
```
npm run translate
```


