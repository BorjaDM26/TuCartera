const prodMode = process.env.NODE_ENV === 'production';
const path = require('path');

module.exports = {
  publicPath: prodMode? process.env.BASE_URL : '/',
  devServer: {
    proxy: {
      '/api': {
        target: 'https://localhost:44306',
      },
    },
  },
  pluginOptions: {
    'style-resources-loader': {
      preProcessor: 'scss',
      patterns: [path.resolve(__dirname, './src/styles/styles.scss')],
    },
  },
  chainWebpack(config) {
    config.resolve.alias.delete("@")
    config.resolve
      .plugin("tsconfig-paths")
      .use(require("tsconfig-paths-webpack-plugin"))
    config.plugin('html').tap(args => {
      args[0].title = 'Tu Cartera';
      return args;
    });
  },
};
