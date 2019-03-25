# Netlenium Package Builder
Netlenium Package Builder is a command-line tool that works with Mono, which
allows you to build Netlenium Packages from a source directory into a .np
file which can be executed by the Netlenium Runtime program

## Usage
```
usage: npbuild [options]
 options:
     -h, --help                  Displays the help menu
     -p, --prompt  true/false    Prompts the user before exiting the process
     -s, --source  required      The source directory which contains the main python script and package metadata
```

## Exit Codes

| Exit Code | Description                          |
|-----------|--------------------------------------|
| 0         | Package Built Successfully           |
| 1         | Help Menu Shown                      |
| 2         | Missing Parameter `source`           |
| 3         | Cannot read package.json             |
| 4         | Error while trying to build .np file |
| 5         | Error deleting old .np file          |
| 6         | package.json missing `name`          |
| 7         | package.json missing `version`       |
| 8         | The source directory does not exist  |
| 9         | File package.json not found          |
| 10        | Missing main.py                      |
| 11        | Error parsing command-line arguments |