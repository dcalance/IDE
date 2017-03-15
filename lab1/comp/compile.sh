#!/bin/bash


FILE=$1
filename=${FILE%.*}

if [ "${FILE: -2}" == ".c" ] && [ -f $FILE ]
	then
	gcc ${FILE} -o ${filename}
	./$filename
elif [ "${FILE: -4}" == ".cpp" ] && [ -f $FILE ]
	then
	g++ -o ${filename} ${FILE} 
	./$filename
elif [ "${FILE: -3}" == ".py" ] && [ -f $FILE ]
	then
	python ${FILE} 
#The class containing main function must have name Main	
elif [ "${FILE: -5}" == ".java" ] && [ -f $FILE ]
	then
	javac ${FILE}
	java "Main"
else
	echo "File not found or bad extenstion.(Accepted extensions : .c, .cpp, .py, .java)"
fi