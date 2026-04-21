using Microsoft.ML;
using System.IO;

namespace Email_Spam_Project.Models
{
    public class SpamDetectionService
    {
        private readonly MLContext _mlContext;
        private ITransformer? _model;
        private PredictionEngine<EmailData, EmailPrediction>? _predictionEngine;

        public SpamDetectionService()
        {
            _mlContext = new MLContext(seed: 1);
            TrainModel();
        }

        private void TrainModel()
        {
            try
            {
                string dataPath = Path.Combine(Directory.GetCurrentDirectory(), "Dataset", "spamdata.csv");
                
                if (!File.Exists(dataPath))
                {
                    return;
                }

                var data = _mlContext.Data.LoadFromTextFile<EmailData>(
                    dataPath,
                    separatorChar: ',',
                    hasHeader: true);

                var pipeline = _mlContext.Transforms.Text
                    .FeaturizeText("Features", nameof(EmailData.Message))
                    .Append(_mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: "Label", featureColumnName: "Features"));

                _model = pipeline.Fit(data);

                _predictionEngine = _mlContext.Model
                    .CreatePredictionEngine<EmailData, EmailPrediction>(_model);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Training failed: {ex.Message}");
            }
        }

        public EmailPrediction Predict(string message)
        {
            if (_predictionEngine == null)
            {
                return new EmailPrediction { Prediction = false, Probability = 0, Score = 0 };
            }

            return _predictionEngine.Predict(new EmailData
            {
                Message = message
            });
        }
    }
}