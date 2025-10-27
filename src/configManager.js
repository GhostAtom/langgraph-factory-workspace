const fs = require('fs');
const path = require('path');

const CONFIG_FILE_PATH = path.resolve(__dirname, 'config.json');

const loadConfig = () => {
  try {
    if (!fs.existsSync(CONFIG_FILE_PATH)) {
      return {};
    }
    const data = fs.readFileSync(CONFIG_FILE_PATH, 'utf8');
    return JSON.parse(data);
  } catch (error) {
    console.error('Error loading configuration:', error);
    return {};
  }
};

const saveConfig = (config) => {
  try {
    const jsonString = JSON.stringify(config, null, 2);
    fs.writeFileSync(CONFIG_FILE_PATH, jsonString);
    console.log('Configuration saved successfully.');
  } catch (error) {
    console.error('Error saving configuration:', error);
  }
};

module.exports = { loadConfig, saveConfig };