# Music List &nbsp; [![Build Status](https://travis-ci.org/zekroTJA/aspmusiclist.svg?branch=master)](https://travis-ci.org/zekroTJA/aspmusiclist) [![](https://img.shields.io/badge/docker-zekro%2Faspmusiclist-16abc9?logo=docker&logoColor=16abc9)](https://hub.docker.com/r/zekro/aspmusiclist)

This is just a small project where I am currently trying to get into ASP.NET Core as REST API and Angular as Front End.

## "Road Map"

- [ ] API Documentation
- [ ] Appropriate error messages in front end
- [ ] Spotify API integration
  *Maybeeee way later because this would be a heck lot of work for such a project.*

- [x] Administration key  
  *Will be created and displayed on creation of a list to delete, chanhe password*
  - [x] Backend Implementation
  - [x] Frontend Implementation
- [x] Spotify Search Link
- [x] Code Documentation
- [x] Rate Limiting

## Build & Deploy

### Docker

You can use the provided [**docker image**](https://hub.docker.com/r/zekro/aspmusiclist) which is always kept up-to-date with `master` on successful [travis-build](https://travis-ci.org/zekroTJA/aspmusiclist/branches).

```
# docker pull zekro/aspmusiclist:latest
```

Then, start a container binding the SQLite database path from `/etc/mlist/database.db` and pass custom configuration via environment variables like in following example:

```
# docker run \
    --detach  \
    --name musiclist \
    --publish 8080:8080 \
    --restart on-failure \
    --volume ${PWD}/mlist/data:/etc/mlist \
    --env ML_LOGGING__LOGLEVEL__DEFAULT=Information \
    --env ML_CONNECTIONSTRINGS_SQLITE="Data Source=/etc/mlist/database.db"
```

### Build Yourself

If you want to build this application yourself to run it outside of docker or with IIS, use following commands to build:

```
$ dotnet build
```

Or to build a standalone assets set:

```
$ dotnet publish -c Release -r linux-x64 --self-contained true
```

Then, you need to build the front end files:

```
$ cd ClientApp
$ npm install
$ npm run build
```

---

Copyright © 2019 zekro Development (Ringo Hoffmann).  
Covered by MIT Licence.