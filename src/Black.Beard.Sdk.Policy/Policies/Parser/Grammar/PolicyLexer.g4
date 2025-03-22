/**
 * json transform engine lexer
 *
 * Copyright (c) 2017-2019 Gael beard <gaelgael5@gmail.com>
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

lexer grammar PolicyLexer;


TRUE           : 'true';
FALSE          : 'false';

PLUS           : '+';
DOT            : '.';
COLON          : ':';
NOT 			   : '!';
PARENT_LEFT  	: '(';
PARENT_RIGHT 	: ')';
BRACKET_LEFT  	: '[';
BRACKET_RIGHT 	: ']';
EQUAL 			: '=';
INEQUAL 		   : '!=';
GREATER 		   : '>';
LESSER 		   : '<';
GREATER_EQUAL 	: '>=';
LESSER_EQUAL 	: '<=';

OR 				: '|';
AND 			   : '&';
COMMA 			: ',';
HAS 				: 'has';
HAS_NOT 			: '!has';
IN 				: 'in';
NOT_IN 			: '!in';


ALIAS 			: 'alias';
POLICY 			: 'policy';
INCLUDE 			: 'include';



STRING                  : ('"') (ESC | SAFECODEPOINT)* '"';
INT                     : [0-9]+;
ID                      : [_A-Za-z][_A-Za-z0-9]*;
IDQUOTED                : '\'' ID '\'';


fragment ESC            : '\\' (["\\/bfnrt] | UNICODE);
fragment SAFECODEPOINT  : ~ ["\\\u0000-\u001F];
fragment UNICODE        : 'u' HEX HEX HEX HEX;
fragment HEX            : [0-9a-fA-F];


MULTI_LINE_COMMENT      : '/*' .*? '*/' -> skip;
LineComment             : '//' ~ [\r\n]* -> skip;

// VARIABLE_NAME : [_A-Za-z][_A-Za-z0-9]* COLON;
// IDLOWCASE : [_a-z] [_a-z0-9]*;
// CODE_STRING :        QUOTE_CODE_STRING .*? QUOTE_CODE_STRING;
// SINGLE_QUOTE_CODE_STRING :  '\'';