SELECT 
	shard.logicalrelid,
	shard.shardid,
	node.nodename,
    node.nodeport
from pg_dist_shard shard
join pg_dist_placement placement
	on placement.shardid = shard.shardid
join pg_dist_node node
	on placement.groupid = node.groupid and node.noderole = 'primary'::noderole;
