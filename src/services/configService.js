const { MongoClient } = require('mongodb');

async function getConfig() {
    const client = new MongoClient(process.env.MONGO_URI, { useNewUrlParser: true, useUnifiedTopology: true });
    try {
        await client.connect();
        const database = client.db('configDB');
        const configCollection = database.collection('config');
        const config = await configCollection.findOne({});
        if (!config) {
            throw new Error('No configuration found.');
        }
        return config;
    } finally {
        await client.close();
    }
}

module.exports = { getConfig };