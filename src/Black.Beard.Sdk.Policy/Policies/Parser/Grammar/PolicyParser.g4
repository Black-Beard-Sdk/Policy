/**
 * json transform engine Parser
 *
 * Copyright (c) 2020-2021 Gael beard <gaelgael5@gmail.com>
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

parser grammar PolicyParser;

options { 
      // memoize=True;
      tokenVocab=PolicyLexer;
    } 

script :
    pair*
    EOF
    ;

pair :
     pair_include
   | pair_alias
   | pair_policy
   ;

pair_include : 
   INCLUDE string
   ;

pair_alias : 
   ALIAS alias_id COLON string
   ;

pair_policy : 
   POLICY categories? policy_id COLON expression
   ;

categories :
     PARENT_LEFT category (COMMA category)* PARENT_RIGHT
   ;

array :
     BRACKET_LEFT value_ref (COMMA value_ref)* BRACKET_RIGHT
   ;

operationBoolean : 
     AND | OR
   ;

operationEqual : 
     EQUAL | INEQUAL | GREATER | LESSER | GREATER_EQUAL | LESSER_EQUAL 
   ;

operationContains : 
     IN | NOT_IN | HAS | HAS_NOT
   ;

expression
   : source? key_ref (PLUS | operationEqual value_ref)?
   | NOT expression
   | PARENT_LEFT expression PARENT_RIGHT
   | expression operationBoolean expression
   | expression operationContains array
   ;

value_ref
   : string
   | IDQUOTED
   | ID
   | boolean
   | integer
   ;

source : (ID | IDQUOTED) DOT;
string : STRING;
integer : INT;
alias_id : ID | IDQUOTED;
policy_id : ID (DOT ID)* | IDQUOTED;

key_ref : ID | IDQUOTED;
category : ID;

boolean : TRUE | FALSE;