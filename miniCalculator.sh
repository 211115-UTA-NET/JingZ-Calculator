#!/usr/bin/bash

while :
do
	echo -n "Enter Your Arithmetic Operations: "
	# IFS used to split a string into words
	read ariOp
	echo "------ Calculation Steps ------"
	echo "  $ariOp"
	IFS=' '  #split input by space
	# -r = define \ as char not escape char
	# -a = define ARR index start at zero
	read -ra ARR <<< $ariOp #reading ariOp as an array as tokens separated by IFS 
	operators=()
	nums=()
	len=${#ARR[@]}
	# separate numbers and operators
	for (( i=0; i<$len; i++ )) 
	do  
		if [ $((i%2)) -eq 0 ]
		then
			nums+=("${ARR[$i]}")
		else
			operators+=("${ARR[$i]}")
		fi
	done
	# Multiplication and division take precedence
	for (( idx=0; idx<${#operators[@]}; ))
	do
		# echo "idx = $idx"
		# multiplication
		if [[ ${operators[$idx]} == "*" ]]
		then
			pre=$((idx*2))
			curr=$((idx*2+1))
			next=$((idx*2+2))
			ARR[curr]=$(( ARR[pre] * ARR[next] ))
			unset operators[idx]
			unset ARR[pre]
			unset ARR[next]
			ARR=("${ARR[@]}")	# rearrange index, cuz unset removes both idx & value
			operators=("${operators[@]}")
			echo "= ${ARR[@]}"
			# echo "${operators[@]}"
		elif [[ ${operators[$idx]} == "/" ]] 		# division
		then
			pre=$((idx*2))
			curr=$((idx*2+1))
			next=$((idx*2+2))
			ARR[curr]=$(( ARR[pre] / ARR[next] ))
			unset operators[idx]
			unset ARR[pre]
			unset ARR[next]
			ARR=("${ARR[@]}")
			operators=("${operators[@]}")
			echo "= ${ARR[@]}"
			# echo "${operators[@]}"
		else
			((idx++))
		fi
	done
	# Addition and Subtraction
	idx=0  # first value of array
	while [ ${#operators[@]} != 0 ]	# while operators array is not empty
	do
		# addition
		if [[ ${operators[$idx]} == "+" ]]
		then
			pre=$((idx*2))
			curr=$((idx*2+1))
			next=$((idx*2+2))
			ARR[curr]=$(( ARR[pre] + ARR[next] ))
			unset operators[idx]
			unset ARR[pre]
			unset ARR[next]
			ARR=("${ARR[@]}")
			operators=("${operators[@]}")
			echo "= ${ARR[@]}"
			# echo "${operators[@]}"
		elif [[ ${operators[$idx]} == "-" ]]		# subtraction
		then
			pre=$((idx*2))
			curr=$((idx*2+1))
			next=$((idx*2+2))
			ARR[curr]=$(( ARR[pre] - ARR[next] ))
			unset operators[idx]
			unset ARR[pre]
			unset ARR[next]
			ARR=("${ARR[@]}")
			operators=("${operators[@]}")
			echo "= ${ARR[@]}"
			# echo "${operators[@]}"
		fi
	done
	echo "-------------------------------"
	# calculate input
	echo -n "Do Anothor Calculation? (y/n) "
	read x
	if [ $x == "Y" ] || [ $x == "y" ]
	then
		continue
	else
		echo "--- Bye! ---";
		break
	fi
done
