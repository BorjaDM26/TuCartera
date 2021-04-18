const path = require('path');

module.exports = {
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
};
