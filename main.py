# Generated code based on plan

```json
{
  "files": [
    {
      "path": "README.md",
      "content": "# Hello World Python Script\n\n## Overview\nThis project is a simple Python script that outputs \"Hello, World!\" to the console. It includes unit tests to ensure the script functions correctly.\n\n## Setup & Installation\n\n1. **Clone the repository**\n   ```bash\n   git clone <repository-url>\n   cd <repository-directory>\n   ```\n\n2. **Set up a virtual environment**\n   ```bash\n   python3 -m venv venv\n   source venv/bin/activate  # On Windows use `venv\\Scripts\\activate`\n   ```\n\n3. **Install dependencies**\n   ```bash\n   pip install pytest\n   ```\n\n## Running the Script\nTo execute the script, run:\n```bash\npython src/hello_world.py\n```\n\n## Running Tests\nTo run the tests, use:\n```bash\npytest\n```\n"
    },
    {
      "path": "src/hello_world.py",
      "content": "def get_greeting():\n    return \"Hello, World!\"\n\nif __name__ == \"__main__\":\n    print(get_greeting())\n"
    },
    {
      "path": "tests/test_hello_world.py",
      "content": "from src.hello_world import get_greeting\nimport subprocess\n\n\ndef test_get_greeting():\n    assert get_greeting() == \"Hello, World!\"\n\n\ndef test_script_output():\n    result = subprocess.run(['python', 'src/hello_world.py'], capture_output=True, text=True)\n    assert result.stdout.strip() == \"Hello, World!\"\n"
    },
    {
      "path": ".gitignore",
      "content": "venv/\n__pycache__/\n*.pyc\n"
    }
  ]
}
```