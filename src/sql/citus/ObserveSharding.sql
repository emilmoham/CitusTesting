select * from pg_dist_shard;

SELECT 
	shard.logicalrelid,
	shard.shardid,
	node.nodename
from pg_dist_shard shard
join pg_dist_placement placement
	on placement.shardid = shard.shardid
join pg_dist_node node
	on placement.groupid = node.groupid and node.noderole = 'primary'::noderole;


select * from facilities f ;

with constants(facility_id) as (
values (8))
select
	shardid,
	shardstate,
	shardlength,
	nodename,
	nodeport,
	placementid
from
	pg_dist_placement as placement,
	pg_dist_node as node,
	constants as c
where
	placement.groupid = node.groupid
	and node.noderole = 'primary'
   AND (
   		   shardid = (SELECT get_shard_id_for_distribution_column('facilities', c.facility_id))
		or shardid = (SELECT get_shard_id_for_distribution_column('accounts', c.facility_id))
		or shardid = (SELECT get_shard_id_for_distribution_column('transactions', c.facility_id))
		or shardid = (SELECT get_shard_id_for_distribution_column('entries', c.facility_id))
   );