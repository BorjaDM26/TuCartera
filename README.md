# Tu Cartera

Tu Cartera project is made up of two blocks, a Vue front-end and a .NET Core API.

<br>

## Front (Vue.js)

The scripts used in front are the following.

### Project setup

```
npm install
```

### Compiles and hot-reloads for development

```
npm run serve
```

### Compiles and minifies for production

```
npm run build
```

### Lints and fixes files

```
npm run lint
```

<br>

## API (.NET Core)

This block includes two modules: main module, where EPs are defined, and DBModel, which includes an adapter to use database.

To make it work, you have to set the following values in `appsettings.json`:

-   `TuCarteraContext`: connection string to your database.
-   `FmpApiKey`: the api key to use Financial Modeling Prep API.
