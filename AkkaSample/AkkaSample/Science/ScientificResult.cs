using System;

namespace AkkaSample.Science
{
    public class ScientificResult
    {
        private readonly OrderDto _controlDto;
        private readonly Guid _correlationId;
        private readonly OrderDto _experimentalDto;
        private readonly TimeSpan _controlTimeSpan;
        private readonly TimeSpan _experimentalTimeSpan;

        public ScientificResult(Guid correlationId, OrderDto controlDto, OrderDto experimentalDto, TimeSpan controlTimeSpan, TimeSpan experimentTimeSpan)
        {
            _correlationId = correlationId;
            _controlDto = controlDto;
            _experimentalDto = experimentalDto;
            _controlTimeSpan = controlTimeSpan;
            _experimentalTimeSpan = experimentTimeSpan;
        }

        public Guid CorrelationId => _correlationId;

        public OrderDto ControlDto => _controlDto;

        public TimeSpan ControlTimeSpan => _controlTimeSpan;

        public OrderDto ExperimentalDto => _experimentalDto;

        public TimeSpan ExperimentalTimeSpan => _experimentalTimeSpan;

    }
}
