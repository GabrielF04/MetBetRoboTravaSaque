using MetBetTravaSaque.AcoesRobo;
using MetBetTravaSaque.ConfiguracoesWeb;
using MetBetTravaSaque.Email;
using PipeliningLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetBetTravaSaque.Pipelines
{
    public class Pipelines : PipelineGroup
    {
        public Pipelines() : base()
        {
            Pipeline("ExecucaoInicial")
               .Pipe<WebConfig>()
               .Pipe<GerenciarHora>()
               ;

            Pipeline("ExecucaoRobo")
                .Pipe<PainelLogin>()
                .Pipe<TravaSaque>()
                .Pipe<EnviaEmail>()
                .Pipe<EncerroRobo>()
                ;
        }
    }
}
