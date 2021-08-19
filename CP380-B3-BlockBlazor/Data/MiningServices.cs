using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CP380_B1_BlockList.Models;

namespace CP380_B3_BlockBlazor.Data
{
    
    
    public class MiningService
    
    
    
    {
        private readonly BlockService _blockService;
        private readonly PendingTransactionService _pendingTransaction;
        public IEnumerable<Block> tmpBlockList { get; set; }
        public IEnumerable<Payload> tmpPayloadList { get; set; }

        public MiningService(BlockService blockService, PendingTransactionService pendingTransactionService)
        {
            _blockService = blockService;
            _pendingTransaction = pendingTransactionService;
        }

       
       
       
       public async Task<Block> MineBlock()
        {

            tmpBlockList = await _blockService.GetBlock();
            tmpPayloadList = await _pendingTransaction.GetPayloads();
            var blk = new Block(DateTime.Now, tmpBlockList.Select(bb => bb.PreviousHash).Last(), tmpPayloadList.ToList());
            blk.Mine(2);

            return block;

        }
    }
}
