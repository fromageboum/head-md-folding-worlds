const fs = require('fs');
const path = require('path');

const directory = process.argv[2]; // Get the directory from the command line arguments
const output_file = process.argv[3]; // Get the output file from the command line arguments

let output = '';

// Get the files in the directory, sort them, and filter out non-markdown files
const inputFiles = fs.readdirSync(directory)
    .filter(file => path.extname(file) === '.md')
    .sort()
    .reverse();

inputFiles.forEach(inputFile => {
    let data = fs.readFileSync(path.join(directory, inputFile), 'utf8');
    output += `\n<!-- File: ${inputFile} -->\n`;
    output += data;
});

fs.writeFileSync(output_file, output);