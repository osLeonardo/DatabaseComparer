# Teste de performance em bancos de dados NoSQL: Apache Cassandra vs. Neo4j

### Curso de Ciência da Computação – Universidade do Extremo Sul Catarinense (UNESC)
### Prof. Marlon de Matos de Oliveira
## Amanda V. Soares, João Vítor F. Sonego, Leonardo O. Spilere

### Diretório 'Tested':
Possui as configurações dos testes rodados no JMeter;
- arquivos com prefixo CAS são referentes ao Apache Cassandra;
- arquivos com prefixo NEO são referentes ao Neo4j;
- arquivos com sufixo Single são referentes aos teste de UM (01) usuário e MIL (1.000) requisições;
- arquivos com sufixo Threads são referentes aos teste de SEISCENTOS (600) usuário e CINCO (05) requisições;

### Diretório 'BancoDeDadosAPI':
Abriga a API REST usada para comunicação com os bancos de dados;

#### Controllers:
- rotas de comunicação da API com os bancos de dados.
  
#### Interfaces:
- lista de métodos da cada banco de dados.
  
#### Models:
- modelo de tranferência de dados padrão.
  
#### Properties:
- configurações de inicialização do projeto.
  
#### Services:
- implementação dos métodos e queries dos bancos de dados.
