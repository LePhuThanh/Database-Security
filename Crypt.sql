--create master key

if not exists 
(
 select *
 from sys.symmetric_keys
 where symmetric_key_id=101
)
create master key encryption by
	password = '1712037'
go

--create private key
if not exists
(
 select *
 from sys.symmetric_keys
 where name = 'PriKey'
)
create symmetric key PriKey
--with algorithm
 with algorithm = AES_256
 encryption by password= '1712037';
go

