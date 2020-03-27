USE PPBeta
GO

INSERT INTO Cursos(Descricao, Ativo)
VALUES ('Analise e desenvolvimento de sitemas', 1);
GO

INSERT INTO Cursos(Descricao, Ativo)
VALUES ('Banco de dados', 1);
GO

INSERT INTO Cursos(Descricao, Ativo)
VALUES ('Teste', 0);
GO

INSERT INTO Alunos(RA, Nome, CursoID, DataNascimento, Email)
VALUES(1802047, 'Erick Novais Da Hora', 1, '10/01/1994', 'erick.hora@aluno.faculdadeimpacta.com.br');
GO

INSERT INTO Usuarios(AlunoId, Login, Senha, Perfil)
VALUES(1, 'erick.hora', '9A16B485EAEAB7F4615B8A631E1F54822DACF2EE95832179E5F639F8E5F1B35', 2);

INSERT INTO Cordenadores(Nome, Email)
VALUES('admin','admin@aluno.faculdadeimpacta.com.br');

INSERT INTO Usuarios(CordenadorID, Login, Senha, Perfil)
VALUES(1, 'admin', '9A16B485EAEAB7F4615B8A631E1F54822DACF2EE95832179E5F639F8E5F1B35', 1);