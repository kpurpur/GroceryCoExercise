
SET IDENTITY_INSERT Promotion OFF;

-- buy 3 apples for $2
insert into Promotion values ('apple', 'p', '2016-01-01', null, 3, null, 2);

-- buy 1 Nutella, get 1 free
insert into Promotion values ('nuttl', 'd', '2016-01-01', null, 1, 1, 0);

-- buy 1 orang, get 1 for 50% off
insert into Promotion values ('orang', 'd', '2016-01-01', null, 1, 1, 0.5);