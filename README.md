# Project-PixBlocks-Addition
Projekt inżynierski

### Krótki kurs korzystania z gita:
- Synchronizowanie swojego brancha z `develop`:
```
git checkout develop
git pull origin develop
git checkout OUR_BRANCH
git rebase develop
```
lub zamiast ostatniej linijki
```
git merge develop
```
Można także deczko krócej:
```
git checkout develop
git pull origin develop
git merge OUR_BRANCH develop
```

- Ustawienie Windows'owego znaku końca linii:
```
git config --global core.autocrlf true
```
