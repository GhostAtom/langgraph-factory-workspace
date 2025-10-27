const { loadConfig, saveConfig } = require('../src/configManager');
const fs = require('fs');

jest.mock('fs');

describe('Configuration Manager', () => {
  beforeEach(() => {
    jest.clearAllMocks();
  });

  test('loadConfig should return config object when file exists', () => {
    const mockConfig = { key: 'value' };
    fs.existsSync.mockReturnValue(true);
    fs.readFileSync.mockReturnValue(JSON.stringify(mockConfig));

    const config = loadConfig();
    expect(config).toEqual(mockConfig);
  });

  test('loadConfig should return empty object when file does not exist', () => {
    fs.existsSync.mockReturnValue(false);

    const config = loadConfig();
    expect(config).toEqual({});
  });

  test('saveConfig should write config object to file', () => {
    const config = { key: 'value' };
    const jsonString = JSON.stringify(config, null, 2);

    saveConfig(config);
    expect(fs.writeFileSync).toHaveBeenCalledWith(expect.any(String), jsonString);
  });
});