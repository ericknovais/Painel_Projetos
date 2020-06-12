USE PPBeta
GO

--Insert Cursos
INSERT INTO Cursos(Descricao, Ativo) VALUES ('Analise e desenvolvimento de sitemas', 1) GO
INSERT INTO Cursos(Descricao, Ativo) VALUES ('Banco de dados', 1)GO
INSERT INTO Cursos(Descricao, Ativo) VALUES ('Teste', 0)GO

--Insert Alunos
INSERT INTO Alunos(Ra, Nome, CursoId, DataNascimento, Email) VALUES(1802047, 'Erick Novais Da Hora', 1, '01/10/1994', 'erick.hora@aluno.faculdadeimpacta.com.br') GO
INSERT INTO Alunos(Ra, Nome, CursoId, DataNascimento, Email) VALUES(1802045, 'Paulo Ferreira', 1, '05/12/1999', 'paulo.ferreira@aluno.faculdadeimpacta.com') GO
INSERT INTO Alunos(Ra, Nome, CursoId, DataNascimento, Email) VALUES(1803304, 'Iago Andrade', 1, '01/01/1994', 'iago.andrade@aluno.faculdadeimpacta.com') GO
INSERT INTO Alunos(Ra, Nome, CursoId, DataNascimento, Email) VALUES(1805560, 'Giovanna Memoli', 1, '01/10/1996', 'giovanna.memoli@aluno.faculdadeimpacta.com') GO
INSERT INTO Alunos(Ra, Nome, CursoId, DataNascimento, Email) VALUES(1806630, 'Robson Cruz', 1, '01/10/1994', 'robson.cruz@aluno.faculdadeimpacta.com.br') GO

--Insert Representantes
INSERT INTO Representantes(Nome, Email) VALUES('Marcelo Riva', 'marcelo.riva@Iteris.com.br') GO
INSERT INTO Representantes(Nome, Email) VALUES('Marcelo cruz', 'marcelo.cruz@faculdadeimpacta.com') GO

--insert Empresa
INSERT INTO Empresas(CNPJ, RazaoSocial, RepresentanteId) VALUES('06.022.456/0001-39', 'Iteris', 1) GO
INSERT INTO Empresas(CNPJ, RazaoSocial, RepresentanteId) VALUES('06.022.456/0001-39', 'impacta', 2) GO

--insert Coordenador
INSERT INTO Cordenadores(Nome, Email)VALUES('admin','admin@aluno.faculdadeimpacta.com.br') GO

-- insert Usuarios
INSERT INTO Usuarios(AlunoID,RepresentanteID, CordenadorID, Login, Senha, Perfil) VALUES(1, NULL, NULL, 'erick.hora','9A16B485EAEAB7F4615B8A631E1F54822DACF2EE95832179E5F639F8E5F1B35', 2) GO
INSERT INTO Usuarios(AlunoID,RepresentanteID, CordenadorID, Login, Senha, Perfil) VALUES(NULL, NULL, 1, 'admin','9A16B485EAEAB7F4615B8A631E1F54822DACF2EE95832179E5F639F8E5F1B35', 1) GO
INSERT INTO Usuarios(AlunoID,RepresentanteID, CordenadorID, Login, Senha, Perfil) VALUES(NULL, 1, NULL, 'marcelo.riva','9A16B485EAEAB7F4615B8A631E1F54822DACF2EE95832179E5F639F8E5F1B35', 3) GO
INSERT INTO Usuarios(AlunoID,RepresentanteID, CordenadorID, Login, Senha, Perfil) VALUES(NULL, 4, NULL, 'teste','9A16B485EAEAB7F4615B8A631E1F54822DACF2EE95832179E5F639F8E5F1B35', 3) GO
INSERT INTO Usuarios(AlunoID,RepresentanteID, CordenadorID, Login, Senha, Perfil) VALUES(NULL, 5, NULL, 'marcelo.cruz','9A16B485EAEAB7F4615B8A631E1F54822DACF2EE95832179E5F639F8E5F1B35', 3) GO
INSERT INTO Usuarios(AlunoID,RepresentanteID, CordenadorID, Login, Senha, Perfil) VALUES(4, NULL, NULL, 'paulo.ferreira','9A16B485EAEAB7F4615B8A631E1F54822DACF2EE95832179E5F639F8E5F1B35', 2) GO
INSERT INTO Usuarios(AlunoID,RepresentanteID, CordenadorID, Login, Senha, Perfil) VALUES(5, NULL, NULL, 'iago.andrade','9A16B485EAEAB7F4615B8A631E1F54822DACF2EE95832179E5F639F8E5F1B35', 2) GO
INSERT INTO Usuarios(AlunoID,RepresentanteID, CordenadorID, Login, Senha, Perfil) VALUES(6, NULL, NULL, 'giovanna.memoli','8D969EEF6ECAD3C29A3A629280E686CFC3F5D5A86AFF3CA122C923ADC6C92', 2) GO
INSERT INTO Usuarios(AlunoID,RepresentanteID, CordenadorID, Login, Senha, Perfil) VALUES(8, NULL, NULL, 'teste','9A16B485EAEAB7F4615B8A631E1F54822DACF2EE95832179E5F639F8E5F1B35', 2) GO
INSERT INTO Usuarios(AlunoID,RepresentanteID, CordenadorID, Login, Senha, Perfil) VALUES(9, NULL, NULL, 'robson.cruz','9A16B485EAEAB7F4615B8A631E1F54822DACF2EE95832179E5F639F8E5F1B35', 2) GO




