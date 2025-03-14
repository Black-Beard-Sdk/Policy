# antlr-4.7-complete.jar must be preset 
# http://www.antlr.org/download/antlr-4.7-complete.jar

java.exe -jar antlr-4.13.2-complete.jar -Dlanguage=CSharp PolicyLexer.g4 -visitor -no-listener -package Bb.Policies.Parser
java.exe -jar antlr-4.13.2-complete.jar -Dlanguage=CSharp PolicyParser.g4 -visitor -no-listener -package Bb.Policies.Parser