# Tu Cartera

Tu Cartera project is made up of two blocks, a .NET Core API and a Vue.js front-end.

<br>

## API (.NET Core)

This block includes two modules:

- `Database model`: contains sql files to create and fill the database, a context to act as a database interface and an adapter to prepare data to be used in API endpoints.
- `Main module`: contains API endpoints which will be used by front end app.

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

## Installation

To test this project in your local machine, you have to follow the next steps.

### Financial Modeling Prep API

1. Create an account in [Financial Modeling Prep API](https://financialmodelingprep.com/developer/docs/pricing). Then you will have access to an FMP API Key.

### Tu Cartera API

2. Insert the FMP API key in the call to `spTickersPopulate` in `/TuCartera/TuCartera.DBModel/SQL/bd_deafult_data.sql`.

3. Create the database in your server. You have to execute `bd_structure` and `bd_default_data` sql files (in that order) to create and populate the database.

4. Set the following values in `appsettings.json`:
   - `TuCarteraContext`: connection string to your database.
   - `FmpApiKey`: the api key to use Financial Modeling Prep API.

### Tu Cartera Front

5. Install dependencies executing the command:

```
npm install
```

### Serve App

6. In the API project, use Visual Studio to start the API.

7. In the front app, execute the command:

```
npm serve
```
