INSERT INTO shoplist (name, description)
  VALUES ('Estoque Black Friday', 'Preparação do estoque para promoção'),
		 ('Feira', 'Produtos para comprar na feira'),
		('Aniversário do João', 'Festa infantil para 30 pessoas');
  

INSERT INTO product (name, brand, description, number, shoplistId)
  VALUES ('Notebook', 'Samsung', 'Intel Core i5', 20, 1),
		('Teclado', 'Multilaser', 'Preto', 40, 1),
		('Mouse', 'Logitech', 'Sem fio', 30, 1),
		('Smartphone', 'iPhone', '14 Pro', 5, 1),
		('HeadPhone', 'Philips', 'Com fio', 10, 1);

INSERT INTO product (name, brand, description, number, shoplistId)
  VALUES ('Banana', 'Sem marca', 'Nanica', 5, 2),
		('Alface', 'Sem marca', 'Lisa', 2, 2),
		('Limão', 'Sem marca', 'Siciliano', 4, 2),
		('Couve', 'Sem marca', 'Observar qualidade', 1, 2),
		('Biscoito', 'Da Vovó', 'Pacote de 200g', 3, 2);

INSERT INTO product (name, brand, description, number, "ShoplistId")
  VALUES ('Bolo', 'Mastrella', 'Chocolate', 1, 3),
		('Brigadeiro', 'Amor e Doces', 'Com granulado', 60, 3),
		('Vela', 'Qualquer', 'Número 8', 1, 3),
		('Refrigerante', 'Coca-Cola', 'Gelada', 5, 3),
		('Cachorro quente', 'Cantina da Ana', 'Mini', 45, 3);